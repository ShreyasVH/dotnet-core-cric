using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class SeriesTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public SeriesTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SeriesType GetById(int id)
        {
            return _dbContext.SeriesTypes.Find(id);
        }
        
        public List<SeriesType> GetByIds(List<int> ids)
        {
            return _dbContext.SeriesTypes.Where(type => ids.Contains(type.Id)).ToList();
        }
    }
}