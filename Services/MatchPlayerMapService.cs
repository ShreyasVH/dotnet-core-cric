using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

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
    }
}