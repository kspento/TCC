using MediatR;
using UserManagement.Data.Repository.UserRepository;
using UserManagement.Data.Resources;
using UserManagement.Repository;

namespace UserManagement.MediatR.Queries
{
    public class GetUsersQuery : IRequest<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
