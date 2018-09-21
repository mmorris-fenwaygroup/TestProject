using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agile.Domain;
using Agile.DAO;
using Agile.Services.Interface;
/// <summary>
/// Summary description for UserImpl
/// </summary>
namespace Agile.Services.Impl {
    public class UserImpl : UserService {
        public UserImpl() {
            //
            // TODO: Add constructor logic here
            //
        }

        #region UserService Members


        public User findUserById(string userId) {
            UserDAO udao = new UserDAO();
            return udao.FindByID(userId);
        }

        public void updateUser(User usr) {
            UserDAO udao = new UserDAO();
            udao.UpdateUser(usr);
        }

        public void insertUser(User usr) {
            UserDAO udao = new UserDAO();
            udao.InsertUser(usr);
        }

        public void insertPendingUser(User usr) {
            UserDAO udao = new UserDAO();
            udao.InsertPendingUser(usr);
        }

        #endregion
    }
}