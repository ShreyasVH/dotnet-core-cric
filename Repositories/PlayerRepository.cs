using System;
using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Players;

namespace Com.Dotnet.Cric.Repositories
{
    public class PlayerRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Player Create(CreateRequest createRequest)
        {
            Player player = new Player(createRequest);
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
            return player;
        }

        public Player GetByNameAndCountryIdAndDateOfBirth(string name, long countryId, DateOnly dateOfBirth)
        {
            return _dbContext.Players.FirstOrDefault(p => p.Name == name && p.CountryId == countryId && p.DateOfBirth == dateOfBirth);
        }
        
        public List<Player> GetAll(int page, int limit)
        {
            return _dbContext.Players.OrderBy(t => t.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Players.Count();
        }
    }
}