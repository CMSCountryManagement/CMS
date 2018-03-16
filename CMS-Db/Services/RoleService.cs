using CMS_Common;
using CMS_Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Db.Services
{
    public class RoleService : BaseService
    {
        public void AddUserRole(Entity.User user)
        {
            var userDetails = Repository.Users.First(u => u.UserName.Equals(user.UserName));
            var role = Repository.Roles.First(r => r.Name.Equals(RoleConstant.User.ToString()));
            Repository.UserRoles.Add(new UserRole() { RoleId = role.Id, IdentityUser_Id = user.Id, UserId = Guid.NewGuid().ToString() });
            Repository.SaveChanges();
        }
    }
}
