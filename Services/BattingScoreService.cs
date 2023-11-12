using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Services
{
    public class BattingScoreService
    {
        private readonly BattingScoreRepository _battingScoreRepository;

        public BattingScoreService(BattingScoreRepository battingScoreRepository)
        {
            _battingScoreRepository = battingScoreRepository;
        }

        public List<BattingScore> Add(List<BattingScoreRequest> battingScoreRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _battingScoreRepository.Add(battingScoreRequests, playerToMatchPlayerMap);
        }

        public Dictionary<string, Dictionary<string, int>> GetDismissalStats(long playerId)
        {
            return _battingScoreRepository.GetDismissalStats(playerId);
        }
        
        public Dictionary<string, Dictionary<string, int>> GetBattingStats(long playerId)
        {
            return _battingScoreRepository.GetBattingStats(playerId);
        }

        public List<BattingScore> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _battingScoreRepository.GetByMatchPlayerIds(matchPlayerIds);
        }
    }
}