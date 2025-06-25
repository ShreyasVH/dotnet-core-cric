using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Players;

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
            //_dbContext.SaveChanges();
            return matchPlayerMaps;
        }

        public List<MatchPlayerMap> GetByMatchId(int matchId)
        {
            return _dbContext.MatchPlayerMaps.Where(mpm => mpm.MatchId.Equals(matchId)).ToList();
        }
        
        public List<MatchPlayerMap> GetByPlayerId(long playerId)
        {
            return _dbContext.MatchPlayerMaps.Where(mpm => mpm.PlayerId.Equals(playerId)).ToList();
        }
        
        public void Remove(int matchId)
        {
            _dbContext.MatchPlayerMaps.RemoveRange(GetByMatchId(matchId));
            _dbContext.SaveChanges();
        }
        
        public void Merge(MergeRequest mergeRequest)
        {
            var matchPlayerMaps = GetByPlayerId(mergeRequest.PlayerIdToMerge);
            matchPlayerMaps.ForEach(mpm => mpm.PlayerId = mergeRequest.OriginalPlayerId);
            _dbContext.SaveChanges();
        }
    }
}