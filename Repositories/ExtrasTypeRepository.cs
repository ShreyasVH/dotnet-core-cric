using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class ExtrasTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public ExtrasTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ExtrasType> GetAll()
        {
            return _dbContext.ExtrasTypes.ToList();
        }
    }
}