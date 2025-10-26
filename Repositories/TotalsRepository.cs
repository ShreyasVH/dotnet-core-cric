using System.Linq;
using System.Collections.Generic;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class TotalsRepository
    {
        private readonly AppDbContext _dbContext;

        public TotalsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(List<Total> totals)
        {
            _dbContext.Totals.AddRange(totals);
        }
        
        public List<Total> GetByMatchId(int matchId)
        {
            return _dbContext.Totals.Where(mpm => mpm.MatchId.Equals(matchId)).ToList();
        }
        
        public void Remove(int matchId)
        {
            _dbContext.Totals.RemoveRange(GetByMatchId(matchId));
            _dbContext.SaveChanges();
        }
    }
}