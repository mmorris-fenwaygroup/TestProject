using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Agile.Helper;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void Page_LoadComplete(object sender, EventArgs e) {
        if (Page.User.Identity.IsAuthenticated) {
            Session["UserID"] = Page.User.Identity.Name.ToString();
        }
    }
}