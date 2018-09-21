using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using Agile.Helper;
using Agile.Domain;

/// <summary>
/// Summary description 
/// </summary>
public class ApplicationRoleProvider : RoleProvider {
    public ApplicationRoleProvider() {
        //
        // TODO: Add constructor logic here
        //
    }

    private string pApplicationName;
    public override string ApplicationName {
        get { return pApplicationName; }
        set { pApplicationName = value; }
    }

    //
    // A helper function to retrieve config values from the configuration file.
    //

    private string GetConfigValue(string configValue, string defaultValue) {
        if (String.IsNullOrEmpty(configValue))
            return defaultValue;

        return configValue;
    }

    public override void Initialize(string name, NameValueCollection config) {
        if (config == null)
            throw new ArgumentNullException("config");

        if (name == null || name.Length == 0)
            name = "OracleRoleProvider";

        if (String.IsNullOrEmpty(config["description"])) {
            config.Remove("description");
            config.Add("description", "Sample Membership provider");
        }

        // Initialize the abstract base class.
        base.Initialize(name, config);

        pApplicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override void CreateRole(string roleName) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override string[] GetAllRoles() {
        throw new Exception("The method or operation is not implemented.");
    }

    public override string[] GetRolesForUser(string username) {
        DataTable tbl = DBHelper.SelectDataTable(UsersRole.SELECT_USERS_ROLE, DBHelper.mp("USER_ID", username));
        string[] roles = new string[tbl.Rows.Count];
        int i = 0;
        foreach (DataRow row in tbl.Rows)
        {
            roles[i++] = row["ROLE_ID"].ToString();
        }

        return roles;
    }

    public override string[] GetUsersInRole(string roleName) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override bool IsUserInRole(string username, string roleName) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
        throw new Exception("The method or operation is not implemented.");
    }

    public override bool RoleExists(string roleName) {
        throw new Exception("The method or operation is not implemented.");
    }
}