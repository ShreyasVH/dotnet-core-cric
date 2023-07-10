using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class FielderDismissalService
    {
        private readonly FielderDismissalRepository _fielderDismissalRepository;

        public FielderDismissalService(FielderDismissalRepository fielderDismissalRepository)
        {
            _fielderDismissalRepository = fielderDismissalRepository;
        }

        public List<FielderDismissal> Add(Dictionary<int, List<long>> scoreFieldersMap, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _fielderDismissalRepository.Add(scoreFieldersMap, playerToMatchPlayerMap);
        }

        public Dictionary<string, Dictionary<string, int>> GetFieldingStats(long playerId)
        {
            return _fielderDismissalRepository.GetFieldingStats(playerId);
        }
    }
}