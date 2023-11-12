using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class CaptainService
    {
        private readonly CaptainRepository _captainRepository;

        public CaptainService(CaptainRepository captainRepository)
        {
            _captainRepository = captainRepository;
        }

        public List<Captain> Add(List<long> playerIds, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _captainRepository.Add(playerIds, playerToMatchPlayerMap);
        }

        public List<Captain> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _captainRepository.GetByMatchPlayerIds(matchPlayerIds);
        }
    }
}