using System.Collections.Generic;
using System.Linq;

using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Repositories
{
    public class TagsRepository
    {
        private readonly AppDbContext _dbContext;

        public TagsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Tag> GetAll(int page, int limit)
        {
            return _dbContext.Tags.OrderBy(c => c.Name).Skip((page - 1) * limit).Take(limit).ToList();
        }

        public int GetTotalCount()
        {
            return _dbContext.Tags.Count();
        }
        
        public List<Tag> GetByIds(List<int> ids)
        {
            return _dbContext.Tags.Where(c => ids.Contains(c.Id)).ToList();
        }
    }
}