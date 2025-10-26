using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
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

        public void Add(int seriesId, List<long> teamIds)
        {
            seriesTeamsMapRepository.Add(seriesId, teamIds);
        }

        public List<SeriesTeamsMap> GetBySeriesIds(List<int> seriesIds)
        {
            return seriesTeamsMapRepository.GetBySeriesIds(seriesIds);
        }

        public void Remove(long seriesId, List<long> teamIds)
        {
            seriesTeamsMapRepository.Remove(seriesId, teamIds);
        }

        public void Remove(int seriesId)
        {
            seriesTeamsMapRepository.Remove(seriesId);
        }
    }
}