using System;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using Agile.Domain;
using Agile.DAO;
using Agile.Helper;
using Agile.Services.Impl;
using Agile.Services.Interface;
using System.Collections;

namespace Agile.DAO {
    public class UsersRoleDAO {

        public UsersRoleDAO() {

        }

        public ArrayList Select(string p) {
            DataTable dt = DBHelper.SelectDataTable(UsersRole.SELECT_USERS_ROLE, DBHelper.mp("USER_ID", p));
            ArrayList q = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++) {
                UsersRole ur = new UsersRole();
                ur.UserId = (((DataRow)(dt.Rows[i]))["USER_ID"]).ToString();
                ur.RoleId = (((DataRow)(dt.Rows[i]))["ROLE_ID"]).ToString();
                ur.Active = (((DataRow)(dt.Rows[i]))["ACTIVE"]).ToString();
                q.Add(ur);
            }
            return q;
        }

        public void Insert(UsersRole p) {
            OracleParameter[] paramsList = createParamList(p);
            DBHelper.Execute(UsersRole.INSERT_USERS_ROLE, paramsList);
        }

        public void Update(UsersRole p) {
            OracleParameter[] paramsList = createParamList(p);
            DBHelper.Execute(UsersRole.UPDATE_USERS_ROLE, paramsList);
        }

        public void Delete(string p) {
            DBHelper.Execute(UsersRole.DELETE_USERS_ROLE, DBHelper.mp("USER_ID", p));
        }

        private OracleParameter[] createParamList(UsersRole p) {
            OracleParameter[] paramsList = new OracleParameter[3];
            paramsList[0] = DBHelper.mp("USER_ID", p.UserId);
            paramsList[1] = DBHelper.mp("ROLE_ID", p.RoleId);
            paramsList[2] = DBHelper.mp("ACTIVE", p.Active);
            return paramsList;
        }
     }
}
