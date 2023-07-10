using System.Collections.Generic;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class FielderDismissalRepository
    {
        private readonly AppDbContext _dbContext;

        public FielderDismissalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<FielderDismissal> Add(Dictionary<int, List<long>> scoreFielderMap, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var fielderDismissals = new List<FielderDismissal>();

            foreach (var scoreId in scoreFielderMap.Keys)
            {
                foreach (var playerId in scoreFielderMap[scoreId])
                {
                    fielderDismissals.Add(new FielderDismissal()
                    {
                        ScoreId = scoreId,
                        MatchPlayerId = playerToMatchPlayerMap[playerId]
                    });
                }
            }
            
            _dbContext.FielderDismissals.AddRange(fielderDismissals);
            _dbContext.SaveChanges();
            return fielderDismissals;
        }
    }
}