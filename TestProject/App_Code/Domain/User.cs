using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for User
/// </summary>
namespace Agile.Domain
{
    public class User {
        private string _userId;
        private string _fName;
        private string _lName;
        private string _email;
        private string _phone;
        private string _active;

        private ArrayList _roles;

        public User() {
            this._roles = new ArrayList();
        }
        public string UserId { get { return _userId; } set { _userId = value; } }
        public string FName { get { return _fName; } set { _fName = value; } }
        public string LName { get { return _lName; } set { _lName = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Active { get { return _active; } set { _active = value; } }
        public ArrayList Roles { get { return _roles; } set { _roles = value; } }

        #region SQL
        public const string CHECK_IF_USER_EXISTS =
        "SELECT COUNT(*) FROM USERS WHERE USER_ID = :id";

        public const string FIND_USER_BY_USER_ID =
            "SELECT USER_ID, FNAME, LNAME, EMAIL, PHONE, ACTIVE FROM USERS WHERE USER_ID = :userId";

        public const string FIND_USER_ROLES_BY_USER_ID =
            "SELECT ROLE_ID FROM USERS_ROLE WHERE USER_ID = :userId AND ACTIVE = 'T'";

        public const string DELETE_USER_ROLES_SQL =
            "DELETE USERS_ROLE WHERE USER_ID = :userId";

        public const string INSERT_USER_ROLES_SQL =
            "INSERT INTO USERS_ROLE (USER_ID, ROLE_ID, ACTIVE) VALUES (:userId, :roleId, :active)";

        public const string INSERT_USER_SQL =
            "INSERT INTO USERS (USER_ID, FNAME, LNAME, PHONE, EMAIL, ACTIVE) VALUES (:userId, :fName, :lName, :phone, :email, :active)";

        public const string UPDATE_USER_SQL =
            "UPDATE USERS SET FNAME = :fName, LNAME = :lName, PHONE = :phone, EMAIL = :email, ACTIVE = :active WHERE USER_ID = :userId";

        public static string GET_FNAME =
            "SELECT FNAME FROM USERS WHERE USER_ID = :userId";
        #endregion
    }
}