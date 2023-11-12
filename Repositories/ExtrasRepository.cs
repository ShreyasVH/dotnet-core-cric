using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class ExtrasRepository
    {
        private readonly AppDbContext _dbContext;

        public ExtrasRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Extras> Add(int matchId, List<ExtrasRequest> extrasRequests)
        {
            var extrasList = extrasRequests.Select(extrasRequest => new Extras(matchId, extrasRequest)).ToList();

            _dbContext.Extras.AddRange(extrasList);
            _dbContext.SaveChanges();
            
            return extrasList;
        }

        public List<Extras> GetByMatchId(int matchId)
        {
            return _dbContext.Extras.Where(e => e.MatchId == matchId).ToList();
        }
    }
}