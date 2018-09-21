using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Web.Configuration;

/// <summary>
/// Summary description for AgileUtils
/// </summary>
namespace Agile.Helper {
    public class AgileUtils {
        #region Alerts
        public static void ShowAlert(string message) {
            string cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert")) {
                page.ClientScript.RegisterClientScriptBlock(typeof(Page), "alert", script);
            }
        }

        public static void ShowAlert(string message, string returnURL) {
            string cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "'); window.location.href='" + returnURL + "';</script>";
            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert")) {
                page.ClientScript.RegisterClientScriptBlock(typeof(Page), "MessagePopUp", script);
            }
        }
        #endregion

        #region Mail
        public static void SendMail(String to, String from, String cc, String body, String subject) {
            MailMessage oMsg = new MailMessage();
            oMsg.From = new System.Net.Mail.MailAddress(from);
            oMsg.To.Add(to);
            if (cc != null) {
                if (!cc.Equals("")) {
                    oMsg.CC.Add(cc);
                }
            }
            oMsg.Subject = (subject);

            // Create message Body.
            oMsg.IsBodyHtml = true;
            oMsg.Body = body;

            // Name of remote SMTP server.
            SmtpClient client = new SmtpClient("mailhost.corp.intranet");
            client.Send(oMsg);

            oMsg = null;
        }
        public static void SendMail(String to, String from, String cc, String body, String subject, String attachment) {
            try {
                MailMessage oMsg = new MailMessage();
                oMsg.From = new System.Net.Mail.MailAddress(from);
                oMsg.To.Add(to);
                if (cc != null) {
                    if (!cc.Equals("")) {
                        oMsg.CC.Add(cc);
                    }
                }
                oMsg.Subject = (subject);

                // Create message Body.
                oMsg.IsBodyHtml = true;
                oMsg.Body = body;

                // ADD AN ATTACHMENT.
                if (attachment != null) {
                    if (!attachment.Equals("")) {
                        // Create  the file attachment for this e-mail message.
                        Attachment data = new Attachment(attachment);
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(attachment);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachment);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(attachment);
                        disposition.FileName = Path.GetFileName(attachment);
                        oMsg.Attachments.Add(data);
                    }
                }

                // Name of remote SMTP server.
                SmtpClient client = new SmtpClient("mailhost.corp.intranet");
                client.Send(oMsg);

                oMsg = null;
            }
            catch (Exception ex) {
                Console.WriteLine("Exception caught.", ex);
            }
        }
        #endregion

        #region ASRV
        public static AgileLDAPService ASRV()
        {
            AgileLDAPService asrv = new AgileLDAPService();
            AuthHeader auth = new AuthHeader();
            auth.Username = WebConfigurationManager.AppSettings["auth.Username"];
            if (auth.Username == "SOME_NAME_GOES_HERE" || auth.Username.Trim() == "")
            {
                throw new AccessViolationException();
            }

            auth.Password = WebConfigurationManager.AppSettings["auth.Password"];
            asrv.AuthHeaderValue = auth;
            return asrv;
        }

        #endregion
        public AgileUtils() {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}