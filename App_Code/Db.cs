using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

public static class Db
{
    private static bool? _useLocalStore = null;
    private static readonly object _lock = new object();
    private static DataSet _store = null;
    private static string _xmlPath = null;

    public static string Cs 
    { 
        get 
        { 
            string env = Environment.GetEnvironmentVariable("UNRAVELED_DB");
            if (!string.IsNullOrEmpty(env)) return env;
            return ConfigurationManager.ConnectionStrings["UnraveledDb"] != null 
                ? ConfigurationManager.ConnectionStrings["UnraveledDb"].ConnectionString 
                : ""; 
        } 
    }

    private static bool IsLocalDbOrEmpty(string cs)
    {
        if (string.IsNullOrEmpty(cs)) return true;
        return cs.IndexOf("LocalDB", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool CheckUseLocalStore()
    {
        if (_useLocalStore.HasValue) return _useLocalStore.Value;

        lock (_lock)
        {
            if (_useLocalStore.HasValue) return _useLocalStore.Value;

            bool isMono = Type.GetType("Mono.Runtime") != null;
            string cs = Cs;

            if (isMono && IsLocalDbOrEmpty(cs))
            {
                InitLocalStore();
                _useLocalStore = true;
                return true;
            }

            _useLocalStore = false;
            return false;
        }
    }

    private static void InitLocalStore()
    {
        if (_store != null) return;

        string baseDir = HttpRuntime.AppDomainAppPath ?? AppDomain.CurrentDomain.BaseDirectory;
        string appData = Path.Combine(baseDir, "App_Data");
        if (!Directory.Exists(appData))
        {
            try { Directory.CreateDirectory(appData); } catch { }
        }
        _xmlPath = Path.Combine(appData, "unraveled_store.xml");

        _store = new DataSet("UnraveledStore");
        DataTable tours = new DataTable("Tours");
        tours.Columns.Add("TourId", typeof(int));
        tours.Columns.Add("TourDate", typeof(DateTime));
        tours.Columns.Add("City", typeof(string));
        tours.Columns.Add("Country", typeof(string));
        tours.Columns.Add("Venue", typeof(string));
        tours.PrimaryKey = new DataColumn[] { tours.Columns["TourId"] };
        _store.Tables.Add(tours);

        DataTable bookings = new DataTable("Bookings");
        bookings.Columns.Add("BookingId", typeof(string));
        bookings.Columns.Add("Email", typeof(string));
        bookings.Columns.Add("FullName", typeof(string));
        bookings.Columns.Add("City", typeof(string));
        bookings.Columns.Add("TourDate", typeof(DateTime));
        bookings.Columns.Add("Venue", typeof(string));
        bookings.Columns.Add("Pass", typeof(string));
        bookings.Columns.Add("Quantity", typeof(int));
        bookings.Columns.Add("TicketTotal", typeof(int));
        bookings.Columns.Add("NightBudget", typeof(int));
        bookings.Columns.Add("Outfit", typeof(string));
        bookings.Columns.Add("CreatedAt", typeof(DateTime));
        bookings.PrimaryKey = new DataColumn[] { bookings.Columns["BookingId"] };
        _store.Tables.Add(bookings);

        if (File.Exists(_xmlPath))
        {
            try
            {
                _store.ReadXml(_xmlPath);
                if (tours.Rows.Count > 0) return;
            }
            catch { }
        }

        SeedTours(tours);
        SaveLocalStore();
    }

    private static void SaveLocalStore()
    {
        if (_store == null || string.IsNullOrEmpty(_xmlPath)) return;
        try
        {
            _store.WriteXml(_xmlPath, XmlWriteMode.WriteSchema);
        }
        catch { }
    }

    private static void SeedTours(DataTable t)
    {
        string[,] raw = {
            {"2026-09-25","Hartford, CT","USA","PeoplesBank Arena"},
            {"2026-09-26","Hartford, CT","USA","PeoplesBank Arena"},
            {"2026-09-29","Pittsburgh, PA","USA","PPG Paints Arena"},
            {"2026-09-30","Pittsburgh, PA","USA","PPG Paints Arena"},
            {"2026-10-03","Washington, D.C.","USA","Capital One Arena"},
            {"2026-10-04","Washington, D.C.","USA","Capital One Arena"},
            {"2026-10-07","Charlotte, NC","USA","Spectrum Center"},
            {"2026-10-08","Charlotte, NC","USA","Spectrum Center"},
            {"2026-10-11","Chicago, IL","USA","United Center"},
            {"2026-10-12","Chicago, IL","USA","United Center"},
            {"2026-10-15","Boston, MA","USA","TD Garden"},
            {"2026-10-17","Boston, MA","USA","TD Garden"},
            {"2026-10-21","Montreal, QC","Canada","Bell Centre"},
            {"2026-10-22","Montreal, QC","Canada","Bell Centre"},
            {"2026-10-26","Toronto, ON","Canada","Scotiabank Arena"},
            {"2026-10-27","Toronto, ON","Canada","Scotiabank Arena"},
            {"2026-10-29","Columbus, OH","USA","Nationwide Arena"},
            {"2026-10-30","Columbus, OH","USA","Nationwide Arena"},
            {"2026-11-07","Philadelphia, PA","USA","Xfinity Mobile Arena"},
            {"2026-11-08","Philadelphia, PA","USA","Xfinity Mobile Arena"},
            {"2026-11-11","Atlanta, GA","USA","State Farm Arena"},
            {"2026-11-12","Atlanta, GA","USA","State Farm Arena"},
            {"2026-11-15","Orlando, FL","USA","Kia Center"},
            {"2026-11-16","Orlando, FL","USA","Kia Center"},
            {"2026-11-19","Sunrise, FL","USA","Amerant Bank Arena"},
            {"2026-11-20","Sunrise, FL","USA","Amerant Bank Arena"},
            {"2026-11-23","Nashville, TN","USA","Bridgestone Arena"},
            {"2026-11-24","Nashville, TN","USA","Bridgestone Arena"},
            {"2026-12-01","Vancouver, BC","Canada","Rogers Arena"},
            {"2026-12-02","Vancouver, BC","Canada","Rogers Arena"},
            {"2026-12-07","Seattle, WA","USA","Climate Pledge Arena"},
            {"2026-12-08","Seattle, WA","USA","Climate Pledge Arena"},
            {"2026-12-11","Oakland, CA","USA","Oakland Arena"},
            {"2026-12-12","Oakland, CA","USA","Oakland Arena"},
            {"2026-12-15","Sacramento, CA","USA","Golden 1 Center"},
            {"2026-12-16","Sacramento, CA","USA","Golden 1 Center"},
            {"2026-12-19","Las Vegas, NV","USA","T-Mobile Arena"},
            {"2026-12-20","Las Vegas, NV","USA","T-Mobile Arena"},
            {"2027-01-12","Los Angeles, CA","USA","Crypto.com Arena"},
            {"2027-01-13","Los Angeles, CA","USA","Crypto.com Arena"},
            {"2027-01-16","Los Angeles, CA","USA","Crypto.com Arena"},
            {"2027-01-17","Los Angeles, CA","USA","Crypto.com Arena"},
            {"2027-02-11","Brooklyn, NY","USA","Barclays Center"},
            {"2027-02-12","Brooklyn, NY","USA","Barclays Center"},
            {"2027-02-15","Brooklyn, NY","USA","Barclays Center"},
            {"2027-02-16","Brooklyn, NY","USA","Barclays Center"},
            {"2027-03-19","Stockholm","Sweden","Avicii Arena"},
            {"2027-03-20","Stockholm","Sweden","Avicii Arena"},
            {"2027-03-23","Amsterdam","Netherlands","Ziggo Dome"},
            {"2027-03-24","Amsterdam","Netherlands","Ziggo Dome"},
            {"2027-04-01","Munich","Germany","Olympiahalle"},
            {"2027-04-02","Munich","Germany","Olympiahalle"},
            {"2027-04-05","London","UK","The O2"},
            {"2027-04-06","London","UK","The O2"},
            {"2027-04-08","London","UK","The O2"},
            {"2027-04-09","London","UK","The O2"},
            {"2027-05-10","London","UK","The O2"},
            {"2027-04-23","Paris","France","Accor Arena"},
            {"2027-04-27","Milan","Italy","Unipol Forum"},
            {"2027-04-28","Milan","Italy","Unipol Forum"},
            {"2027-05-01","Barcelona","Spain","Palau Sant Jordi"},
            {"2027-05-02","Barcelona","Spain","Palau Sant Jordi"}
        };

        for (int i = 0; i < raw.GetLength(0); i++)
        {
            t.Rows.Add(i + 1, DateTime.Parse(raw[i, 0]), raw[i, 1], raw[i, 2], raw[i, 3]);
        }
    }

    public static DataTable Query(string sql, params SqlParameter[] p)
    {
        if (CheckUseLocalStore())
        {
            return QueryLocal(sql, p);
        }

        try
        {
            var dt = new DataTable();
            using (var c = new SqlConnection(Cs))
            using (var a = new SqlDataAdapter(sql, c))
            {
                a.SelectCommand.Parameters.AddRange(p);
                a.Fill(dt);
            }
            return dt;
        }
        catch (PlatformNotSupportedException)
        {
            lock (_lock)
            {
                InitLocalStore();
                _useLocalStore = true;
            }
            return QueryLocal(sql, p);
        }
    }

    public static int Exec(string sql, params SqlParameter[] p)
    {
        if (CheckUseLocalStore())
        {
            return ExecLocal(sql, p);
        }

        try
        {
            using (var c = new SqlConnection(Cs))
            using (var m = new SqlCommand(sql, c))
            {
                m.Parameters.AddRange(p);
                c.Open();
                return m.ExecuteNonQuery();
            }
        }
        catch (PlatformNotSupportedException)
        {
            lock (_lock)
            {
                InitLocalStore();
                _useLocalStore = true;
            }
            return ExecLocal(sql, p);
        }
    }

    private static DataTable QueryLocal(string sql, SqlParameter[] p)
    {
        lock (_lock)
        {
            InitLocalStore();
            Dictionary<string, object> paramMap = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (p != null)
            {
                foreach (var param in p)
                {
                    string k = param.ParameterName.StartsWith("@") ? param.ParameterName.Substring(1) : param.ParameterName;
                    paramMap[k] = param.Value;
                }
            }

            if (sql.IndexOf("FROM Tours", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable src = _store.Tables["Tours"];
                DataTable dt = src.Clone();

                if (paramMap.ContainsKey("i"))
                {
                    int id = Convert.ToInt32(paramMap["i"]);
                    DataRow r = src.Rows.Find(id);
                    if (r != null) dt.ImportRow(r);
                    return dt;
                }

                DataView dv = new DataView(src);
                if (paramMap.ContainsKey("q"))
                {
                    string qVal = (paramMap["q"] ?? "").ToString().Replace("%", "").Trim();
                    if (!string.IsNullOrEmpty(qVal))
                    {
                        dv.RowFilter = string.Format("City LIKE '%{0}%' OR Country LIKE '%{0}%'", qVal.Replace("'", "''"));
                    }
                }
                dv.Sort = "TourDate ASC";

                foreach (DataRowView rowView in dv)
                {
                    dt.ImportRow(rowView.Row);
                }
                return dt;
            }

            if (sql.IndexOf("FROM Bookings", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable src = _store.Tables["Bookings"];
                DataTable dt = src.Clone();

                DataView dv = new DataView(src);
                if (paramMap.ContainsKey("e"))
                {
                    string email = (paramMap["e"] ?? "").ToString().Trim();
                    dv.RowFilter = string.Format("Email = '{0}'", email.Replace("'", "''"));
                }
                dv.Sort = "CreatedAt DESC";

                foreach (DataRowView rowView in dv)
                {
                    dt.ImportRow(rowView.Row);
                }
                return dt;
            }

            return new DataTable();
        }
    }

    private static int ExecLocal(string sql, SqlParameter[] p)
    {
        lock (_lock)
        {
            InitLocalStore();
            Dictionary<string, object> paramMap = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (p != null)
            {
                foreach (var param in p)
                {
                    string k = param.ParameterName.StartsWith("@") ? param.ParameterName.Substring(1) : param.ParameterName;
                    paramMap[k] = param.Value;
                }
            }

            if (sql.IndexOf("INSERT INTO Tours", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable t = _store.Tables["Tours"];
                int nextId = 1;
                foreach (DataRow r in t.Rows)
                {
                    int id = Convert.ToInt32(r["TourId"]);
                    if (id >= nextId) nextId = id + 1;
                }

                DateTime d = Convert.ToDateTime(paramMap.ContainsKey("d") ? paramMap["d"] : paramMap["TourDate"]);
                string city = (paramMap.ContainsKey("c") ? paramMap["c"] : paramMap["City"]).ToString();
                string country = (paramMap.ContainsKey("o") ? paramMap["o"] : paramMap["Country"]).ToString();
                string venue = (paramMap.ContainsKey("v") ? paramMap["v"] : paramMap["Venue"]).ToString();

                t.Rows.Add(nextId, d, city, country, venue);
                SaveLocalStore();
                return 1;
            }

            if (sql.IndexOf("UPDATE Tours", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable t = _store.Tables["Tours"];
                int id = Convert.ToInt32(paramMap["TourId"]);
                DataRow r = t.Rows.Find(id);
                if (r != null)
                {
                    r["TourDate"] = Convert.ToDateTime(paramMap["TourDate"]);
                    r["City"] = paramMap["City"].ToString();
                    r["Country"] = paramMap["Country"].ToString();
                    r["Venue"] = paramMap["Venue"].ToString();
                    SaveLocalStore();
                    return 1;
                }
                return 0;
            }

            if (sql.IndexOf("DELETE FROM Tours", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable t = _store.Tables["Tours"];
                int id = Convert.ToInt32(paramMap["TourId"]);
                DataRow r = t.Rows.Find(id);
                if (r != null)
                {
                    t.Rows.Remove(r);
                    SaveLocalStore();
                    return 1;
                }
                return 0;
            }

            if (sql.IndexOf("INSERT INTO Bookings", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable b = _store.Tables["Bookings"];
                b.Rows.Add(
                    paramMap["id"].ToString(),
                    paramMap["em"].ToString(),
                    paramMap["nm"].ToString(),
                    paramMap["c"].ToString(),
                    Convert.ToDateTime(paramMap["d"]),
                    paramMap["v"].ToString(),
                    paramMap["p"].ToString(),
                    Convert.ToInt32(paramMap["q"]),
                    Convert.ToInt32(paramMap["tt"]),
                    Convert.ToInt32(paramMap["nb"]),
                    paramMap["o"].ToString(),
                    DateTime.Now
                );
                SaveLocalStore();
                return 1;
            }

            if (sql.IndexOf("DELETE FROM Bookings", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable b = _store.Tables["Bookings"];
                string id = paramMap["id"].ToString();
                DataRow r = b.Rows.Find(id);
                if (r != null)
                {
                    b.Rows.Remove(r);
                    SaveLocalStore();
                    return 1;
                }
                return 0;
            }

            return 0;
        }
    }

    public static bool IsAdmin(HttpSessionState s)
    {
        string admin = Environment.GetEnvironmentVariable("ADMIN_EMAIL") 
                    ?? ConfigurationManager.AppSettings["AdminEmail"] 
                    ?? "admin@unraveled.fan";
        return string.Equals(s["Email"] as string, admin, StringComparison.OrdinalIgnoreCase);
    }
}
