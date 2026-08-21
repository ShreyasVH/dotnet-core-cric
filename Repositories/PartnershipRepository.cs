using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class PartnershipRepository : CustomRepository
    {
        public PartnershipRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public List<Partnership> Add(List<PartnershipRequest> partnershipRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var partnerships = partnershipRequests
                .SelectMany(partnershipRequest => new[]
                {
                    new Partnership(partnershipRequest, playerToMatchPlayerMap, true),
                    new Partnership(partnershipRequest, playerToMatchPlayerMap, false)
                })
                .ToList();
            _dbContext.Partnerships.AddRange(partnerships);
            //_dbContext.SaveChanges();
            return partnerships;
        }

        private List<Partnership> GetByMatchPlayerIdsAll(List<int> matchPlayerIds)
        {
            return _dbContext.Partnerships
                .Where(p => matchPlayerIds.Contains(p.MatchPlayerId1) || matchPlayerIds.Contains(p.MatchPlayerId2))
                .ToList();
        }
        
        public List<Partnership> GetByMatchPlayerIds(List<int> matchPlayerIds)
        {
            return _dbContext.Partnerships
                .Where(p => matchPlayerIds.Contains(p.MatchPlayerId1) || matchPlayerIds.Contains(p.MatchPlayerId2))
                .Where(p => p.PrimaryEntry)
                .ToList();
        }
        
        public void Remove(List<int> matchPlayerIds)
        {
            _dbContext.Partnerships.RemoveRange(GetByMatchPlayerIdsAll(matchPlayerIds));
            _dbContext.SaveChanges();
        }
    }
}