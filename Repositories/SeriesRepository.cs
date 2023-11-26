using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Series;

namespace Com.Dotnet.Cric.Repositories
{
    public class SeriesRepository
    {
        private readonly AppDbContext _dbContext;

        public SeriesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Series Create(CreateRequest createRequest)
        {
            Series series = new Series(createRequest);
            _dbContext.Series.Add(series);
            _dbContext.SaveChanges();
            return series;
        }

        public Series GetByNameAndTourIdAndGameTypeId(string name, long tourId, int gameTypeId)
        {
            return _dbContext.Series.FirstOrDefault(s => s.Name == name && s.TourId == tourId && s.GameTypeId == gameTypeId);
        }
        
        public List<Series> GetAll(int page, int limit)
        {
            return _dbContext.Series.OrderBy(t => t.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Series.Count();
        }

        public Series GetById(long id)
        {
            return _dbContext.Series.Find(id);
        }

        public void Update(Series series)
        {
            _dbContext.SaveChanges();
        }
        
        public List<Series> GetByTourId(long tourId)
        {
            return _dbContext.Series.Where(s => s.TourId == tourId).OrderByDescending(s => s.StartTime).ToList();
        }

        public void Remove(long id)
        {
            _dbContext.Series.Remove(GetById(id));
            _dbContext.SaveChanges();
        }
    }
}