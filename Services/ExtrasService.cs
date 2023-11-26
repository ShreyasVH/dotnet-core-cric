using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Services
{
    public class ExtrasService
    {
        private readonly ExtrasRepository _extrasRepository;

        public ExtrasService(ExtrasRepository extrasRepository)
        {
            _extrasRepository = extrasRepository;
        }

        public List<Extras> Add(int matchId, List<ExtrasRequest> extrasRequests)
        {
            return _extrasRepository.Add(matchId, extrasRequests);
        }

        public List<Extras> GetByMatchId(int matchId)
        {
            return _extrasRepository.GetByMatchId(matchId);
        }

        public void Remove(int matchId)
        {
            _extrasRepository.Remove(matchId);
        }
    }
}