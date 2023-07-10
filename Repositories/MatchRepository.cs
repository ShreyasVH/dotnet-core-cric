using System;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class MatchRepository
    {
        private readonly AppDbContext _dbContext;

        public MatchRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Match Create(CreateRequest createRequest)
        {
            Match match = new Match(createRequest);
            _dbContext.Matches.Add(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match GetByStadiumAndStartTime(long stadiumId, DateTime startTime)
        {
            return _dbContext.Matches.FirstOrDefault(m => m.StadiumId == stadiumId && m.StartTime.Equals(startTime));
        }
    }
}