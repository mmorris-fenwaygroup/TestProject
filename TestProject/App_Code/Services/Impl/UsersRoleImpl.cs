using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agile.Domain;
using Agile.DAO;
using Agile.Services.Interface;
using System.Collections;

namespace Agile.Services.Impl {
    public class UsersRoleImpl : UsersRoleService {
        public UsersRoleImpl() {

    }

        #region UsersRoleService Members

        public ArrayList Select(string p)
        {
            UsersRoleDAO q = new UsersRoleDAO();
            return q.Select(p);
        }

        public void Update(UsersRole p) {
            UsersRoleDAO q = new UsersRoleDAO();
            q.Update(p);
        }

        public void Insert(UsersRole p) {
            UsersRoleDAO q = new UsersRoleDAO();
            q.Insert(p);
        }

        public void Delete(string p) {
            UsersRoleDAO q = new UsersRoleDAO();
            q.Delete(p);
        }

        #endregion
    }
}
