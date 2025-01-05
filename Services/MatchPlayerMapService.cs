using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Players;

namespace Com.Dotnet.Cric.Services
{
    public class MatchPlayerMapService
    {
        private readonly MatchPlayerMapRepository _matchPlayerMapRepository;

        public MatchPlayerMapService(MatchPlayerMapRepository matchPlayerMapRepository)
        {
            _matchPlayerMapRepository = matchPlayerMapRepository;
        }

        public List<MatchPlayerMap> Add(int matchId, List<long> playerIds, Dictionary<long, long> playerTeamMap)
        {
            return _matchPlayerMapRepository.Add(matchId, playerIds, playerTeamMap);
        }

        public List<MatchPlayerMap> GetByMatchId(int matchId)
        {
            return _matchPlayerMapRepository.GetByMatchId(matchId);
        }
        
        public void Remove(int matchId)
        {
            _matchPlayerMapRepository.Remove(matchId);
        }

        public void Merge(MergeRequest mergeRequest)
        {
            _matchPlayerMapRepository.Merge(mergeRequest);
        }
    }
}