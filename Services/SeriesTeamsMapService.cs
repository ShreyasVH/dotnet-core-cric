using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class SeriesTeamsMapService
    {
        private readonly SeriesTeamsMapRepository seriesTeamsMapRepository;

        public SeriesTeamsMapService(SeriesTeamsMapRepository seriesTeamsMapRepository)
        {
            this.seriesTeamsMapRepository = seriesTeamsMapRepository;
        }

        public void Add(long seriesId, List<long> teamIds)
        {
            seriesTeamsMapRepository.Add(seriesId, teamIds);
        }
    }
}