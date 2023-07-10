using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class ResultTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public ResultTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultType GetById(int id)
        {
            return _dbContext.ResultTypes.Find(id);
        }
    }
}