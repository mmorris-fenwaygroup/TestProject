using System;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using Agile.Domain;
using Agile.DAO;
using Agile.Helper;

/// <summary>
/// Summary description for UserDAO
/// </summary>
namespace Agile.DAO {
    public class UserDAO {
        
        public UserDAO() {
            //
            // TODO: Add constructor logic here
            //
        }

        public User FindByID(string userId) {
            User usr = new User();
            DataTable dt = DBHelper.SelectDataTable(User.FIND_USER_BY_USER_ID, DBHelper.mp("userId", userId));
            for (int i = 0; i < dt.Rows.Count; i++) {
                usr.UserId = (((DataRow)(dt.Rows[i]))["USER_ID"]).ToString();
                usr.FName = (((DataRow)(dt.Rows[i]))["FNAME"]).ToString();
                usr.LName = (((DataRow)(dt.Rows[i]))["LNAME"]).ToString();
                usr.Phone = (((DataRow)(dt.Rows[i]))["PHONE"]).ToString();
                usr.Email = (((DataRow)(dt.Rows[i]))["EMAIL"]).ToString();
                usr.Active= (((DataRow)(dt.Rows[i]))["ACTIVE"]).ToString();
            }
            setUserRoles(usr);
            return usr;
        }

        public void InsertUser(User usr) {
            OracleParameter[] userParamsList = createUserParamList(usr);
            DBHelper.Execute(User.INSERT_USER_SQL, userParamsList);
            insertRoles(usr, "T");
        }

        public void InsertPendingUser(User usr) {
            OracleParameter[] userParamsList = createUserParamList(usr);
            DBHelper.Execute(User.INSERT_USER_SQL, userParamsList);
            insertRoles(usr, "F");
        }

        public void UpdateUser(User usr) {
            OracleParameter[] userParamsList = createUserParamList(usr);
            DBHelper.Execute(User.UPDATE_USER_SQL, userParamsList);

            DBHelper.Execute(User.DELETE_USER_ROLES_SQL, DBHelper.mp("userId", usr.UserId));
            insertRoles(usr, "T");
        }

        private void insertRoles(User usr, string active) {
            System.Collections.IEnumerator myEnumerator = usr.Roles.GetEnumerator();
            while (myEnumerator.MoveNext()) {
                OracleParameter[] roleParamsList = createRoleParamList(myEnumerator.Current.ToString(), usr.UserId, active);
                DBHelper.Execute(User.INSERT_USER_ROLES_SQL, roleParamsList);
            }
        }


        private OracleParameter[] createUserParamList(User usr) {
            OracleParameter[] paramsList = new OracleParameter[6];
            paramsList[0] = DBHelper.mp("userId", usr.UserId);
            paramsList[1] = DBHelper.mp("fName", usr.FName);
            paramsList[2] = DBHelper.mp("lName", usr.LName);
            paramsList[3] = DBHelper.mp("phone", usr.Phone);
            paramsList[4] = DBHelper.mp("email", usr.Email);
            paramsList[5] = DBHelper.mp("active", usr.Active);

            return paramsList;
        }
 
        private OracleParameter[] createRoleParamList(string role, string userId, string active) {
            OracleParameter[] paramsList = new OracleParameter[3];
            paramsList[0] = DBHelper.mp("userId", userId);
            paramsList[1] = DBHelper.mp("roleId", role);
            paramsList[2] = DBHelper.mp("active", active);

            return paramsList;
        }
        private void setUserRoles(User usr) {
            DataTable dt = DBHelper.SelectDataTable(User.FIND_USER_ROLES_BY_USER_ID, DBHelper.mp("userId", usr.UserId));
            for (int i = 0; i < dt.Rows.Count; i++) {
                usr.Roles.Add((((DataRow)(dt.Rows[i]))["ROLE_ID"]).ToString());
            }
        }
    }
}