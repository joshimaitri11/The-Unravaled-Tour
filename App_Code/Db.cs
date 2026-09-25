using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

public static class Db
{
    public static string Cs 
    { 
        get 
        { 
            string env = Environment.GetEnvironmentVariable("UNRAVELED_DB");
            if (!string.IsNullOrEmpty(env)) return env;
            return ConfigurationManager.ConnectionStrings["UnraveledDb"].ConnectionString; 
        } 
    }

    public static DataTable Query(string sql, params SqlParameter[] p)
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

    public static int Exec(string sql, params SqlParameter[] p)
    {
        using (var c = new SqlConnection(Cs))
        using (var m = new SqlCommand(sql, c))
        {
            m.Parameters.AddRange(p);
            c.Open();
            return m.ExecuteNonQuery();
        }
    }

    public static bool IsAdmin(HttpSessionState s)
    {
        return string.Equals(s["Email"] as string, ConfigurationManager.AppSettings["AdminEmail"], StringComparison.OrdinalIgnoreCase);
    }
}
