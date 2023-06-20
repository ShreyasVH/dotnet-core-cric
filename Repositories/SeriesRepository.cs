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
    }
}