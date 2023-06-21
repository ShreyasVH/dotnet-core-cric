using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class ManOfTheSeriesService
    {
        private readonly ManOfTheSeriesRepository _manOfTheSeriesRepository;

        public ManOfTheSeriesService(ManOfTheSeriesRepository manOfTheSeriesRepository)
        {
            this._manOfTheSeriesRepository = manOfTheSeriesRepository;
        }

        public void Add(long seriesId, List<long> playerId)
        {
            _manOfTheSeriesRepository.Add(seriesId, playerId);
        }

        public List<ManOfTheSeries> GetBySeriesIds(List<long> seriesIds)
        {
            return _manOfTheSeriesRepository.GetBySeriesIds(seriesIds);
        }
        
        public void Remove(long seriesId, List<long> playerId)
        {
            _manOfTheSeriesRepository.Remove(seriesId, playerId);
        }
    }
}