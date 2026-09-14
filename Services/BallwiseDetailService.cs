using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Services
{
    public class BallwiseDetailService
    {
        private readonly BallwiseDetailRepository _ballwiseDetailRepository;

        public BallwiseDetailService(BallwiseDetailRepository ballwiseDetailRepository)
        {
            _ballwiseDetailRepository = ballwiseDetailRepository;
        }

        public List<BallwiseDetail> Add(List<BallwiseDetailRequest> ballwiseDetailRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _ballwiseDetailRepository.Add(ballwiseDetailRequests, playerToMatchPlayerMap);
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _ballwiseDetailRepository.Remove(matchPlayerIds);
        }
        //
        // public List<Partnership> Get(List<int> matchPlayerIds)
        // {
        //     return _partnershipRepository.GetByMatchPlayerIds(matchPlayerIds);
        // }
    }
}