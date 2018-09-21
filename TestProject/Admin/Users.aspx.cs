using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Oracle.DataAccess.Client;
using Agile.Services.Interface;
using Agile.Services.Impl;
using Agile.Domain;
using Agile.Helper;
using System.Web.Services;

public partial class Admin_Users : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void LinkButton3_Click(object sender, EventArgs e) {
        LinkButton lb = (LinkButton)sender;
        userIdHF.Value = lb.Text;
        populateUser(lb.Text);
        mpeUser.Show();
        PanelEdit.Visible = true;
        PanelAdd.Visible = false;
    }

    private void populateUser(string id) {
        UserService usrSrv = new UserImpl();
        User usr = usrSrv.findUserById(id);

        LBLuserId.Text = usr.UserId;
        TXTfName.Text = usr.FName;
        TXTlName.Text = usr.LName;
        TXTphone.Text = usr.Phone;
        TXTemail.Text = usr.Email;
        ddlActive.SelectedValue = usr.Active;

        System.Collections.IEnumerator myEnumerator = usr.Roles.GetEnumerator();
        while (myEnumerator.MoveNext()) {
            CheckBoxList ctl = CBLroles;
            foreach (ListItem item in ctl.Items) {
                if (item.Value.Equals(myEnumerator.Current)) {
                    item.Selected = true;
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        if (searchTextBox.Text == "")
            searchHF.Value = "%";
        else
            searchHF.Value = searchTextBox.Text;
        GridView1.DataBind();
    }
    protected void clearButton_Click(object sender, EventArgs e) {
        searchTextBox.Text = "";
        searchHF.Value = "%";
        GridView1.DataBind();
    }
    protected void BTNsubmit_Click(object sender, EventArgs e) {
        if (isValidData()) {
            User usr = new User();

            usr.UserId = (PanelAdd.Visible ? TXTuserId.Text : LBLuserId.Text);
            usr.FName = TXTfName.Text;
            usr.LName = TXTlName.Text;
            usr.Phone = (TXTphone.Text.Equals("(___) ___-____") ? "" : TXTphone.Text);
            usr.Email = TXTemail.Text;
            usr.Active = ddlActive.SelectedValue;

            CheckBoxList ctrl = CBLroles;
            foreach (ListItem item in ctrl.Items) {
                if (item.Selected) {
                    usr.Roles.Add(item.Value.ToString());
                }
            }
            UserService usrSrv = new UserImpl();

            if (PanelAdd.Visible) {
                usrSrv.insertUser(usr);
            }
            else {
                usrSrv.updateUser(usr);
            }
            mpeUser.Hide();
            clearControls();
            GridView1.DataBind();
        }
        else {
            LBLerror.Visible = true;
            mpeUser.Show();
        }
    }
    protected void BTNcancel_Click(object sender, EventArgs e) {
        mpeUser.Hide();
        clearControls();
    }
    protected void TXTuserId_TextChanged(object sender, EventArgs e) {
        TXTuserId.Text = TXTuserId.Text.ToLower();
        decimal cnt = DBHelper.SelectDecimal(Agile.Domain.User.CHECK_IF_USER_EXISTS, DBHelper.mp("id", TXTuserId.Text));

        if (cnt > 0) {
            populateUser(TXTuserId.Text);
            PanelEdit.Visible = true;
            PanelAdd.Visible = false;
        }
        else {
            AgileLDAPService ldap = AgileUtils.ASRV();
            LDAPInfo li = ldap.GetLDAPInfoByAdId(TXTuserId.Text);
            TXTfName.Text = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(li.FirstName.ToLower());
            TXTlName.Text = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(li.LastName.ToLower());
            if (li.Phone.Length >= 10)
                TXTphone.Text = String.Format("{0:(###) ###-####}", Convert.ToDecimal(li.Phone.Substring(0, 10)));
            TXTemail.Text = li.EMail.ToLower();
        }
        mpeUser.Show();
    }
    private void clearControls() {
        PanelAdd.Visible = true;
        PanelEdit.Visible = false;
        LBLuserId.Text = TXTfName.Text = TXTlName.Text = TXTphone.Text = TXTemail.Text = TXTuserId.Text = String.Empty;
        CBLroles.CssClass = TXTfName.CssClass = TXTlName.CssClass = TXTuserId.CssClass = String.Empty;
        LBLerror.Visible = false;
        CheckBoxList ctl = CBLroles;
        foreach (ListItem item in ctl.Items) {
            item.Selected = false;
        }
    }
    private bool isValidData() {
        bool valid = true;
        if (PanelAdd.Visible) {
            if (TXTuserId.Text.Equals("")) {
                LBLerror.Text += "User ID must be entered.<br>";
                TXTuserId.CssClass = "error";
                valid = false;
            }
        }
        if (TXTfName.Text.Equals("")) {
            LBLerror.Text += "A First Name must be entered.<br>";
            TXTfName.CssClass = "error";
            valid = false;
        }
        if (TXTlName.Text.Equals("")) {
            LBLerror.Text += "A Last Name must be entered.<br>";
            TXTlName.CssClass = "error";
            valid = false;
        }
        bool selected = false;
        CheckBoxList ctl = CBLroles;
        foreach (ListItem item in ctl.Items) {
            if (item.Selected)
                selected = true;
        }
        if (!selected) {
            LBLerror.Text += "At least one user role must be selected.<br>";
            CBLroles.CssClass = "error";
            valid = false;
        }
        return valid;
    }
}
