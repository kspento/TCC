using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UserManagement.Data.UnitOfWork
{
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        int Save();
        Task<int> SaveAsync();
        TContext Context { get; }
    }
}
