using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public partial class Plan : System.Web.UI.Page
{
    static readonly string[] Passes = { "General Admission", "Lower Bowl", "Pit VIP" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        DataTable d = Db.Query("SELECT TourId, TourDate, City, Venue FROM Tours ORDER BY TourDate");
        foreach (DataRow r in d.Rows)
        {
            string text = ((DateTime)r["TourDate"]).ToString("MMM d, yyyy") + " | " + r["City"] + " | " + r["Venue"];
            ddlShow.Items.Add(new ListItem(text, r["TourId"].ToString()));
        }
        ListItem pick = ddlShow.Items.FindByValue(Request.QueryString["id"] ?? "");
        if (pick != null) ddlShow.SelectedValue = pick.Value;
        lblEmail.Text = (string)Session["Email"];
    }

    int TicketCost() { return int.Parse(rblPass.SelectedValue) * int.Parse(ddlQty.SelectedValue); }
    int ExtrasCost() { return cblExtras.Items.Cast<ListItem>().Where(i => i.Selected).Sum(i => int.Parse(i.Value)); }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        int tt = TicketCost(), xt = ExtrasCost();
        lblTickets.Text = "$" + tt;
        lblExtras.Text = "$" + xt;
        lblTotal.Text = "$" + (tt + xt).ToString("N0");

        if (ddlShow.Items.Count > 0)
        {
            DataTable t = Db.Query("SELECT TourDate FROM Tours WHERE TourId=@i", new SqlParameter("@i", int.Parse(ddlShow.SelectedValue)));
            if (t.Rows.Count > 0)
            {
                int days = (int)Math.Ceiling(((DateTime)t.Rows[0]["TourDate"] - DateTime.Today).TotalDays);
                lblCd.Text = days > 0 ? days + " days to go" : "show day is here";
            }
        }

        DataTable mine = Db.Query("SELECT * FROM Bookings WHERE Email=@e ORDER BY CreatedAt DESC", new SqlParameter("@e", (string)Session["Email"]));
        rptMine.DataSource = mine;
        rptMine.DataBind();
        lblNoTix.Visible = mine.Rows.Count == 0;
    }

    protected void btnBook_Click(object sender, EventArgs e)
    {
        lblErr.Text = "";
        string name = txtName.Text.Trim();
        if (name.Length == 0) { lblErr.Text = "Please enter your name"; return; }
        if (ddlShow.Items.Count == 0) { lblErr.Text = "No shows available"; return; }

        DataTable t = Db.Query("SELECT City, TourDate, Venue FROM Tours WHERE TourId=@i", new SqlParameter("@i", int.Parse(ddlShow.SelectedValue)));
        if (t.Rows.Count == 0) { lblErr.Text = "Choose a show"; return; }

        DataRow r = t.Rows[0];
        string id = "UNR-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        int qty = int.Parse(ddlQty.SelectedValue), tt = TicketCost(), xt = ExtrasCost();
        string pass = Passes[rblPass.SelectedIndex], vibe = ddlVibe.SelectedItem.Text;
        DateTime date = (DateTime)r["TourDate"];

        Db.Exec("INSERT INTO Bookings (BookingId, Email, FullName, City, TourDate, Venue, Pass, Quantity, TicketTotal, NightBudget, Outfit) " +
                "VALUES (@id, @em, @nm, @c, @d, @v, @p, @q, @tt, @nb, @o)",
            new SqlParameter("@id", id), new SqlParameter("@em", (string)Session["Email"]), new SqlParameter("@nm", name),
            new SqlParameter("@c", (string)r["City"]), new SqlParameter("@d", date), new SqlParameter("@v", (string)r["Venue"]),
            new SqlParameter("@p", pass), new SqlParameter("@q", qty), new SqlParameter("@tt", tt),
            new SqlParameter("@nb", tt + xt), new SqlParameter("@o", vibe));

        litTicket.Text = "<div class='conf'><h2>YOU'RE GOING!</h2><dl>" +
            Row("Artist", "Olivia Rodrigo") + Row("City", (string)r["City"]) + Row("Date", date.ToString("MMM d, yyyy")) +
            Row("Venue", (string)r["Venue"]) + Row("Ticket", pass) + Row("Quantity", qty.ToString()) +
            Row("Total", "$" + tt) + Row("Night budget", "$" + (tt + xt)) + Row("Outfit", vibe) + Row("Booking ID", id) +
            "</dl><p class='hw'>see you in the front row, " + HttpUtility.HtmlEncode(name) + "</p></div>";
    }

    static string Row(string k, string v)
    {
        return "<dt>" + k + "</dt><dd>" + HttpUtility.HtmlEncode(v) + "</dd>";
    }

    protected void rptMine_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cancel")
            Db.Exec("DELETE FROM Bookings WHERE BookingId=@id AND Email=@e",
                new SqlParameter("@id", (string)e.CommandArgument), new SqlParameter("@e", (string)Session["Email"]));
    }
}
