using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class DismissalModeRepository
    {
        private readonly AppDbContext _dbContext;

        public DismissalModeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<DismissalMode> GetAll()
        {
            return _dbContext.DismissalModes.ToList();
        }
    }
}