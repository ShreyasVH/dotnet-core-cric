using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class CaptainRepository
    {
        private readonly AppDbContext _dbContext;

        public CaptainRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Captain> Add(List<long> playerIds, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var captains = playerIds.Select(playerId => new Captain()
            {
                MatchPlayerId =  playerToMatchPlayerMap[playerId]
            }).ToList();

            _dbContext.Captains.AddRange(captains);
            //_dbContext.SaveChanges();
            
            return captains;
        }

        public List<Captain> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _dbContext.Captains.Where(c => matchPlayerIds.Contains(c.MatchPlayerId)).ToList();
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.Captains.RemoveRange(GetByMatchPlayerIds(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}