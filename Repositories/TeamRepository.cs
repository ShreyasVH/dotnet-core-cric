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
            return _dbContext.Teams.FirstOrDefault(c => c.Name == name && c.CountryId == countryId && c.TypeId == typeId);
        }
    }
}
