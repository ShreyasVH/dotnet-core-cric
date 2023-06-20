using System.Collections.Generic;
using System.Linq;
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
        
        public List<GameType> GetByIds(List<int> ids)
        {
            return _dbContext.GameTypes.Where(gameType => ids.Contains(gameType.Id)).ToList();
        }
    }
}