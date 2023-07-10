using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Repositories
{
    public class BattingScoreRepository
    {
        private readonly AppDbContext _dbContext;

        public BattingScoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BattingScore> Add(List<BattingScoreRequest> battingScoreRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var battingScores = battingScoreRequests.Select(battingScoreRequest => new BattingScore(battingScoreRequest, playerToMatchPlayerMap)).ToList();
            _dbContext.BattingScores.AddRange(battingScores);
            _dbContext.SaveChanges();
            return battingScores;
        }
    }
}