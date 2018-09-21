using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agile.Domain;
using System.Collections;

namespace Agile.Services.Interface {
    public interface UsersRoleService {
        ArrayList Select(string p);
        void Update(UsersRole p);
        void Insert(UsersRole p);
        void Delete(string p);
    }
}
