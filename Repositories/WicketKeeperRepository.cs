using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class WicketKeeperRepository
    {
        private readonly AppDbContext _dbContext;

        public WicketKeeperRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WicketKeeper> Add(List<long> playerIds, Dictionary<long, int> playerToMatchPlayerMap)
        {
            var wicketKeepers = playerIds.Select(playerId => new WicketKeeper()
            {
                MatchPlayerId =  playerToMatchPlayerMap[playerId]
            }).ToList();

            _dbContext.WicketKeepers.AddRange(wicketKeepers);
            _dbContext.SaveChanges();
            
            return wicketKeepers;
        }
    }
}