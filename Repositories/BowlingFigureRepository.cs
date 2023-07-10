using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class BowlingFigureRepository
    {
        private readonly AppDbContext _dbContext;

        public BowlingFigureRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BowlingFigure> Add(List<BowlingFigureRequest> bowlingFigureRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var bowlingFigures = bowlingFigureRequests.Select(bowlingFigureRequest =>
                new BowlingFigure(bowlingFigureRequest, playerToMatchPlayerMap)).ToList();
            _dbContext.BowlingFigures.AddRange(bowlingFigures);
            _dbContext.SaveChanges();
            return bowlingFigures;
        }
    }
}