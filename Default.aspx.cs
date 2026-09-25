using System;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Session["Email"] != null)
            Response.Redirect("~/Home.aspx");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim().ToLower();
        if (!Regex.IsMatch(email, @"^\S+@\S+\.\S+$"))
        {
            lblErr.Text = "Enter a valid email address";
            return;
        }
        Session["Email"] = email;
        Response.Redirect("~/Home.aspx");
    }
}
