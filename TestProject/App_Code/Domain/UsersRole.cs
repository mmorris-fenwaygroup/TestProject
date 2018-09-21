using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Agile.Domain
{
    public class UsersRole {

        private string _userId;
        private string _roleId;
        private string _active;

        public UsersRole() {

        }

        public string UserId { get { return _userId; } set { _userId = value; } }
        public string RoleId { get { return _roleId; } set { _roleId = value; } }
        public string Active { get { return _active; } set { _active = value; } }

        #region SQL
        public const string SELECT_USERS_ROLE =
            "SELECT USER_ID, ROLE_ID, ACTIVE FROM USERS_ROLE WHERE USER_ID = :USER_ID";
        public const string INSERT_USERS_ROLE =
            "INSERT INTO USERS_ROLE(USER_ID, ROLE_ID, ACTIVE) VALUES (:USER_ID, :ROLE_ID, :ACTIVE)";
        public const string UPDATE_USERS_ROLE =
            "UPDATE USERS_ROLE SET ROLE_ID = :ROLE_ID, ACTIVE = :ACTIVE WHERE USER_ID = :USER_ID";
        public const string DELETE_USERS_ROLE = 
            "DELETE FROM USERS_ROLE WHERE USER_ID = :USER_ID";
        #endregion
    }
}
