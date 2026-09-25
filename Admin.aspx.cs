using System;
using System.Data.SqlClient;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Db.IsAdmin(Session))
            Response.Redirect("~/Home.aspx");

        sdsTours.ConnectionString = Db.Cs;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime d;
        if (!DateTime.TryParse(txtDate.Text, out d) || txtCity.Text.Trim() == "" || txtCountry.Text.Trim() == "" || txtVenue.Text.Trim() == "")
        {
            lblMsg.Text = "Fill in all four fields";
            return;
        }
        Db.Exec("INSERT INTO Tours (TourDate, City, Country, Venue) VALUES (@d, @c, @o, @v)",
            new SqlParameter("@d", d), new SqlParameter("@c", txtCity.Text.Trim()),
            new SqlParameter("@o", txtCountry.Text.Trim()), new SqlParameter("@v", txtVenue.Text.Trim()));
        txtDate.Text = txtCity.Text = txtCountry.Text = txtVenue.Text = "";
        lblMsg.Text = "";
        gvTours.DataBind();
    }
}
