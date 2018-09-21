using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
using Agile.Helper;
using System.Net;

public partial class MasterPage : System.Web.UI.MasterPage {
    protected void Page_Load(object sender, EventArgs e) {
        LOGO.Attributes.Add("title", Dns.GetHostName());
    }
    protected void Logout_Click(object sender, EventArgs e) {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("http://home.corp.intranet");
    }
    protected void Login_Click(object sender, EventArgs e) {
        Response.Redirect("~/Secure/Login.aspx?ReturnUrl=" + Request.Url.PathAndQuery.ToString());
    }
}