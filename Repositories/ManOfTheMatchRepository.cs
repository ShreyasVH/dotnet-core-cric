using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class ManOfTheMatchRepository
    {
        private readonly AppDbContext _dbContext;

        public ManOfTheMatchRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ManOfTheMatch> Add(List<long> playerIds, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var manOfTheMatchList = playerIds.Select(playerId => new ManOfTheMatch()
            {
                MatchPlayerId =  playerToMatchPlayerMap[playerId]
            }).ToList();

            _dbContext.ManOfTheMatch.AddRange(manOfTheMatchList);
            //_dbContext.SaveChanges();
            
            return manOfTheMatchList;
        }

        public List<ManOfTheMatch> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _dbContext.ManOfTheMatch.Where(motm => matchPlayerIds.Contains(motm.MatchPlayerId)).ToList();
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.ManOfTheMatch.RemoveRange(GetByMatchPlayerIds(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}