﻿using System;
using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Tours;

namespace Com.Dotnet.Cric.Repositories
{
    public class TourRepository: CustomRepository
    {
        public TourRepository(AppDbContext dbContext): base(dbContext)
        {
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
        
        public List<Tour> GetByIds(List<long> ids)
        {
            return _dbContext.Tours.Where(tour => ids.Contains(tour.Id)).ToList();
        }
        
        public List<Tour> GetAllForYear(int year, int page, int limit)
        {
            DateTime startTime = new DateTime(year, 1, 1, 0, 0, 0);
            DateTime endTime = new DateTime(year, 12, 31, 23, 59, 59);
            return _dbContext.Tours.Where(tour => tour.StartTime >= startTime && tour.StartTime <= endTime).OrderByDescending(t => t.StartTime).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCountForYear(int year)
        {
            DateTime startTime = new DateTime(year, 1, 1, 0, 0, 0);
            DateTime endTime = new DateTime(year, 12, 31, 23, 59, 59);
            return _dbContext.Tours.Count(tour => tour.StartTime >= startTime && tour.StartTime <= endTime);
        }

        public List<int> GetAllYears()
        {
            var years = new List<int>();
            var sql = "SELECT DISTINCT (YEAR(StartTime)) AS year FROM Tours ORDER BY year DESC";
            var result = ExecuteQuery(sql);

            foreach (var row in result)
            {
                years.Add(Convert.ToInt32(row["year"]));
            }

            return years;
        }
    }
}
