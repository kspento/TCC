using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetTotalUserCountQueryHandler : IRequestHandler<GetTotalUserCountQuery, int>
    {
        private readonly IUserRepository _userRepository;
        public GetTotalUserCountQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<int> Handle(GetTotalUserCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.All.Count());
        }
    }
}
