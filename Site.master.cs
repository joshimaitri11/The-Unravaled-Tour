using System;
using System.Web.UI;

public partial class SiteMaster : MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["Email"] == null)
            Response.Redirect("~/Default.aspx", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lnkAdmin.Visible = Db.IsAdmin(Session);
    }

    protected void lnkOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
}
