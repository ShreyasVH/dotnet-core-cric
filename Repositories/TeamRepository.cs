using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Teams;

namespace Com.Dotnet.Cric.Repositories
{
    public class TeamRepository
    {
        private readonly AppDbContext _dbContext;

        public TeamRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Team Create(CreateRequest createRequest)
        {
            Team team = new Team(createRequest);
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();
            return team;
        }

        public Team GetByNameAndCountryIdAndTypeId(string name, long countryId, int typeId)
        {
            return _dbContext.Teams.FirstOrDefault(t => t.Name == name && t.CountryId == countryId && t.TypeId == typeId);
        }

        public List<Team> GetAll(int page, int limit)
        {
            return _dbContext.Teams.OrderBy(t => t.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Teams.Count();
        }
    }
}
