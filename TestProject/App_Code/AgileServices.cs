
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using Agile.Helper;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AgileServices : WebService
{
    public AgileServices()
    {
    }

    [WebMethod]
    public bool Ping() {
        return true;
    }

    [WebMethod]
    public string[] AutoCompleteList(string prefixText, int count) {
        string lname = "";
        string fname = "";
        String[] name = prefixText.Split(',');
        if (name.Length == 2) {
            lname = name[0];
            fname = name[1].Replace(" ", "");
        } else
            lname = name[0];
        AgileLDAPService ldap = AgileUtils.ASRV();
        LDAPInfo[] ldi = ldap.GetLDAPInfoByName(lname, fname);

        List<string> items = new List<string>(ldi.Length);
        foreach (LDAPInfo l in ldi) {
            try {
                items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(l.DisplayName, l.AdId.ToLower()));
                if (items.Count == 100)
                    break;
            } catch (Exception) { }
        }

        return items.ToArray();
    }
}

