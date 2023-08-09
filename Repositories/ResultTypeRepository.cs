using System.Collections.Generic;
using System.Linq;
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
        
        public List<ResultType> GetByIds(List<int> ids)
        {
            return _dbContext.ResultTypes.Where(rt => ids.Contains(rt.Id)).ToList();
        }
    }
}