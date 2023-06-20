using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class SeriesTeamsMapRepository
    {
        private readonly AppDbContext _dbContext;

        public SeriesTeamsMapRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(long seriesId, List<long> teamIds)
        {
            List<SeriesTeamsMap> seriesTeamsMaps = teamIds.Select(teamId => new SeriesTeamsMap(seriesId, teamId)).ToList();
            _dbContext.SeriesTeamsMap.AddRange(seriesTeamsMaps);
            _dbContext.SaveChanges();
        }
    }
}