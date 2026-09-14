using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class BallwiseDetailRepository
    {
        protected readonly AppDbContext _dbContext;
        
        public BallwiseDetailRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<BallwiseDetail> Add(List<BallwiseDetailRequest> ballwiseDetailRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var ballwiseDetailsList = ballwiseDetailRequests
                .Select(ballwiseDetailRequest => new BallwiseDetail(ballwiseDetailRequest, playerToMatchPlayerMap))
                .ToList();
            _dbContext.BallwiseDetails.AddRange(ballwiseDetailsList);
            //_dbContext.SaveChanges();
            return ballwiseDetailsList;
        }

        private List<BallwiseDetail> GetByMatchPlayerIdsAll(List<int> matchPlayerIds)
        {
            return _dbContext.BallwiseDetails
                .Where(bd => matchPlayerIds.Contains(bd.BatsmanMatchPlayerId) || matchPlayerIds.Contains(bd.BowlerMatchPlayerId))
                .ToList();
        }
        
        // public List<Partnership> GetByMatchPlayerIds(List<int> matchPlayerIds)
        // {
        //     return _dbContext.Partnerships
        //         .Where(p => matchPlayerIds.Contains(p.MatchPlayerId1) || matchPlayerIds.Contains(p.MatchPlayerId2))
        //         .Where(p => p.PrimaryEntry)
        //         .ToList();
        // }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.BallwiseDetails.RemoveRange(GetByMatchPlayerIdsAll(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}