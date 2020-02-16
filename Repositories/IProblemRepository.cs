using System.Collections.Generic;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using ExpertalSystem.Mongo;

namespace ExpertalSystem.Repositories
{
    public interface IProblemRepository : IRepository<Problem>
    {
        Task<IEnumerable<Problem>> FindAsync(IssueType type);
    }
}
