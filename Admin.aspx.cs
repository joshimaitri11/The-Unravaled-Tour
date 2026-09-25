using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Db.IsAdmin(Session))
            Response.Redirect("~/Home.aspx");

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        gvTours.DataSource = Db.Query("SELECT TourId, TourDate, City, Country, Venue FROM Tours ORDER BY TourDate");
        gvTours.DataBind();
    }

    protected void gvTours_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTours.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void gvTours_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTours.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvTours_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTours.EditIndex = -1;
        BindGrid();
    }

    protected void gvTours_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int tourId = Convert.ToInt32(gvTours.DataKeys[e.RowIndex].Value);
        GridViewRow row = gvTours.Rows[e.RowIndex];

        TextBox txtDate = (TextBox)row.Cells[0].Controls[0];
        TextBox txtCity = (TextBox)row.Cells[1].Controls[0];
        TextBox txtCountry = (TextBox)row.Cells[2].Controls[0];
        TextBox txtVenue = (TextBox)row.Cells[3].Controls[0];

        DateTime d;
        if (!DateTime.TryParse(txtDate.Text, out d))
        {
            lblMsg.Text = "Invalid date format.";
            return;
        }

        Db.Exec("UPDATE Tours SET TourDate=@TourDate, City=@City, Country=@Country, Venue=@Venue WHERE TourId=@TourId",
            new SqlParameter("@TourDate", d),
            new SqlParameter("@City", txtCity.Text.Trim()),
            new SqlParameter("@Country", txtCountry.Text.Trim()),
            new SqlParameter("@Venue", txtVenue.Text.Trim()),
            new SqlParameter("@TourId", tourId));

        gvTours.EditIndex = -1;
        lblMsg.Text = "";
        BindGrid();
    }

    protected void gvTours_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int tourId = Convert.ToInt32(gvTours.DataKeys[e.RowIndex].Value);
        Db.Exec("DELETE FROM Tours WHERE TourId=@TourId", new SqlParameter("@TourId", tourId));
        BindGrid();
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
        BindGrid();
    }
}
