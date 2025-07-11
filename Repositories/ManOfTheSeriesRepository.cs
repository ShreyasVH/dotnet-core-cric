using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Players;

namespace Com.Dotnet.Cric.Repositories
{
    public class ManOfTheSeriesRepository
    {
        private readonly AppDbContext _dbContext;

        public ManOfTheSeriesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(long seriesId, List<long> playerIds)
        {
            List<ManOfTheSeries> manOfTheSeriesList = playerIds.Select(playerId => new ManOfTheSeries(seriesId, playerId)).ToList();
            _dbContext.ManOfTheSeries.AddRange(manOfTheSeriesList);
            //_dbContext.SaveChanges();
        }

        public List<ManOfTheSeries> GetBySeriesIds(List<long> seriesIds)
        {
            return _dbContext.ManOfTheSeries.Where(mots => seriesIds.Contains(mots.SeriesId)).ToList();
        }
        
        public List<ManOfTheSeries> GetByPlayerId(long playerId)
        {
            return _dbContext.ManOfTheSeries.Where(mots => playerId == mots.PlayerId).ToList();
        }

        public void Remove(long seriesId, List<long> playerIds)
        {
            List<ManOfTheSeries> manOfTheSeriesList = _dbContext.ManOfTheSeries.Where(mots => mots.SeriesId == seriesId && playerIds.Contains(mots.PlayerId)).ToList();
            _dbContext.ManOfTheSeries.RemoveRange(manOfTheSeriesList);
            //_dbContext.SaveChanges();
        }

        public void Remove(long seriesId)
        {
            _dbContext.ManOfTheSeries.RemoveRange(GetBySeriesIds(new List<long>{ seriesId }));
            _dbContext.SaveChanges();
        }

        public void Merge(MergeRequest mergeRequest)
        {
            var manOfTheSeriesList = GetByPlayerId(mergeRequest.PlayerIdToMerge);
            manOfTheSeriesList.ForEach(mots => mots.PlayerId = mergeRequest.OriginalPlayerId);
        }
    }
}