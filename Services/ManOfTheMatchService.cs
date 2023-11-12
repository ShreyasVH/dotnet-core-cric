using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class ManOfTheMatchService
    {
        private readonly ManOfTheMatchRepository _manOfTheMatchRepository;

        public ManOfTheMatchService(ManOfTheMatchRepository manOfTheMatchRepository)
        {
            _manOfTheMatchRepository = manOfTheMatchRepository;
        }

        public List<ManOfTheMatch> Add(List<long> playerIds, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _manOfTheMatchRepository.Add(playerIds, playerToMatchPlayerMap);
        }

        public List<ManOfTheMatch> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _manOfTheMatchRepository.GetByMatchPlayerIds(matchPlayerIds);
        }
    }
}