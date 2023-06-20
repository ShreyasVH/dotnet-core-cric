using System;
using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Tours;

namespace Com.Dotnet.Cric.Repositories
{
    public class TourRepository
    {
        private readonly AppDbContext _dbContext;

        public TourRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Tour Create(CreateRequest createRequest)
        {
            Tour tour = new Tour(createRequest);
            _dbContext.Tours.Add(tour);
            _dbContext.SaveChanges();
            return tour;
        }

        public Tour GetByNameAndStartTime(string name, DateTime startTime)
        {
            return _dbContext.Tours.FirstOrDefault(c => c.Name == name && c.StartTime.Equals(startTime));
        }
        
        public Tour GetById(long id)
        {
            return _dbContext.Tours.Find(id);
        }
    }
}
