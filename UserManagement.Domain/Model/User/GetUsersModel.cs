using System.Collections.Generic;
using UserManagement.Data.Resources;
using UserManagement.Repository;

namespace UserManagement.Domain.Model.User
{
    public class GetUsersModel : UserList
    {
        public UserResource UserResource { get; set; }
    }
}
