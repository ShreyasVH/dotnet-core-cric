using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class WinMarginTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public WinMarginTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WinMarginType GetById(int id)
        {
            return _dbContext.WinMarginTypes.Find(id);
        }
        
        public List<WinMarginType> GetByIds(List<int> ids)
        {
            return _dbContext.WinMarginTypes.Where(wmt => ids.Contains(wmt.Id)).ToList();
        }
    }
}