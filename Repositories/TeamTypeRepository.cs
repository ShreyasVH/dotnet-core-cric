using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class TeamTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public TeamTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TeamType GetById(int id)
        {
            return _dbContext.TeamTypes.Find(id);
        }

        public List<TeamType> GetByIds(List<int> ids)
        {
            return _dbContext.TeamTypes.Where( tt => ids.Contains(tt.Id)).ToList();
        }
    }
}
