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
    }
}