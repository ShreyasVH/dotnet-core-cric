using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Countries;

namespace Com.Dotnet.Cric.Repositories
{
    public class CountryRepository
    {
        private readonly AppDbContext _dbContext;

        public CountryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Country Create(CreateRequest createRequest)
        {
            Country country = new Country(createRequest);
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return country;
        }

        public Country GetByName(string name)
        {
            return _dbContext.Countries.FirstOrDefault(c => c.Name == name);
        }

        public List<Country> GetByNamePattern(string name)
        {
            return _dbContext.Countries.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public List<Country> GetAll(int page, int limit)
        {
            return _dbContext.Countries.OrderBy(c => c.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Countries.Count();
        }

        public Country GetById(long id)
        {
            return _dbContext.Countries.Find(id);
        }
    }
}
