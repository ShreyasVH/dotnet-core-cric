using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class GameTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public GameTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GameType GetById(int id)
        {
            return _dbContext.GameTypes.Find(id);
        }
    }
}