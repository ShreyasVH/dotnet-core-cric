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
    }
}