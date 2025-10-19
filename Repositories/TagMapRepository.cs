using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class TagMapRepository
    {
        private readonly AppDbContext _dbContext;

        public TagMapRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(string entityType, int entityId, List<int> tagIds)
        {
            var tagMaps = tagIds.Select(tagId => new TagMap(entityType, entityId, tagId)).ToList();
            _dbContext.TagMap.AddRange(tagMaps);
            //_dbContext.SaveChanges();
        }
    }
}