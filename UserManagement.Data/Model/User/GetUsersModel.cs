using System.Collections.Generic;
using UserManagement.Data.Repository.UserRepository;
using UserManagement.Data.Resources;

namespace UserManagement.Domain.Model.User
{
    public class GetUsersModel : List<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
