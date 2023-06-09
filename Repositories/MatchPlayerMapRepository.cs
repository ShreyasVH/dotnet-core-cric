using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class MatchPlayerMapRepository
    {
        private readonly AppDbContext _dbContext;

        public MatchPlayerMapRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MatchPlayerMap> Add(int matchId, List<long> playerIds, Dictionary<long, long> playerTeamMap)
        {
            var matchPlayerMaps = playerIds.Select(playerId => new MatchPlayerMap
            {
                MatchId = matchId,
                PlayerId = playerId,
                TeamId = playerTeamMap[playerId]
            }).ToList();
            _dbContext.MatchPlayerMaps.AddRange(matchPlayerMaps);
            _dbContext.SaveChanges();
            return matchPlayerMaps;
        }
    }
}