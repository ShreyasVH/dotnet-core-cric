using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Services
{
    public class PartnershipService
    {
        private readonly PartnershipRepository _partnershipRepository;

        public PartnershipService(PartnershipRepository partnershipRepository)
        {
            _partnershipRepository = partnershipRepository;
        }

        public List<Partnership> Add(List<PartnershipRequest> partnershipRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _partnershipRepository.Add(partnershipRequests, playerToMatchPlayerMap);
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _partnershipRepository.Remove(matchPlayerIds);
        }

        public List<Partnership> Get(List<int> matchPlayerIds)
        {
            return _partnershipRepository.GetByMatchPlayerIds(matchPlayerIds);
        }
    }
}