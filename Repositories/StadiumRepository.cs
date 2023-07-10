﻿using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Stadiums;

namespace Com.Dotnet.Cric.Repositories
{
    public class StadiumRepository
    {
        private readonly AppDbContext _dbContext;

        public StadiumRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Stadium Create(CreateRequest createRequest)
        {
            Stadium stadium = new Stadium(createRequest);
            _dbContext.Stadiums.Add(stadium);
            _dbContext.SaveChanges();
            return stadium;
        }

        public Stadium GetByNameAndCountryId(string name, long countryId)
        {
            return _dbContext.Stadiums.FirstOrDefault(s => s.Name == name && s.CountryId == countryId);
        }

        public List<Stadium> GetAll(int page, int limit)
        {
            return _dbContext.Stadiums.OrderBy(c => c.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Stadiums.Count();
        }

        public Stadium GetById(long id)
        {
            return _dbContext.Stadiums.Find(id);
        }
    }
}
