using System;
using System.Data;
using System.Data.SqlClient;

public partial class Tours : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();
    }

    void Bind()
    {
        DataTable dt = Db.Query(
            "SELECT TourId, TourDate, City, Country, Venue FROM Tours WHERE City LIKE @q OR Country LIKE @q ORDER BY TourDate",
            new SqlParameter("@q", "%" + txtQ.Text.Trim() + "%"));
        rptTours.DataSource = dt;
        rptTours.DataBind();
        lblNone.Visible = dt.Rows.Count == 0;
    }

    protected string StubColor(int i)
    {
        string[] c = { "var(--bl)", "var(--lv)", "var(--pk)" };
        return c[i % 3];
    }
}
