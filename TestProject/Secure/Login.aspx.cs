using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.Configuration;
using Agile.Helper;


public partial class _Default : System.Web.UI.Page {
    protected void Page_PreInit(object sender, EventArgs e) {
        if (Request.QueryString["key"] == null) {
            string authType = WebConfigurationManager.AppSettings["authType"];
            string userIdType = WebConfigurationManager.AppSettings["userIdType"];
            if (authType.Equals("DEV")) {
                Response.Redirect(WebConfigurationManager.AppSettings["SignOnUrlDev"] + userIdType + WebConfigurationManager.AppSettings["SignOnReturn"] + HttpContext.Current.Request.Url.AbsoluteUri);
            } else if (Request.Browser.Browser.Equals("IE") && authType.Equals("WINDOWS")) {
                Response.Redirect(WebConfigurationManager.AppSettings["SignOnUrlWin"] + userIdType + WebConfigurationManager.AppSettings["SignOnReturn"] + HttpContext.Current.Request.Url.AbsoluteUri);
            } else {
                Response.Redirect(WebConfigurationManager.AppSettings["SignOnUrlForms"] + userIdType + WebConfigurationManager.AppSettings["SignOnReturn"] + HttpContext.Current.Request.Url.AbsoluteUri);
            }
        }
        else
        {
            string key = Request.QueryString["key"];

            AgileLDAPService asrv = AgileUtils.ASRV();
            string userId = asrv.SignOnUserId(key);
            LDAPInfo l = new LDAPInfo();

            if (Membership.ValidateUser(userId, "password"))
            {
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userId, DateTime.Now,
                DateTime.Now.AddMinutes(30), false, userId);
                string cookiestr = FormsAuthentication.Encrypt(tkt);
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);

                if (Request["ReturnUrl"] == null)
                    Response.Redirect("~/default.aspx", true);
                else
                    Response.Redirect((String)Request["ReturnUrl"], true);

            }
            else
                Response.Redirect("~/Secure/unauthorized.aspx");
        }
    }
}
