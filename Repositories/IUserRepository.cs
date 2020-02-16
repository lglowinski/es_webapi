using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;

namespace ExpertalSystem.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string login, string password);
    }
}
