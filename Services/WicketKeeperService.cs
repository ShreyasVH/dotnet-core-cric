using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class WicketKeeperService
    {
        private readonly WicketKeeperRepository _wicketKeeperRepository;

        public WicketKeeperService(WicketKeeperRepository wicketKeeperRepository)
        {
            _wicketKeeperRepository = wicketKeeperRepository;
        }

        public List<WicketKeeper> Add(List<long> playerIds, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _wicketKeeperRepository.Add(playerIds, playerToMatchPlayerMap);
        }
    }
}