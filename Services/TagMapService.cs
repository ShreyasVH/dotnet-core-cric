using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class TagMapService
    {
        private readonly TagMapRepository tagMapRepository;

        public TagMapService(TagMapRepository tagMapRepository)
        {
            this.tagMapRepository = tagMapRepository;
        }

        public void Add(string entityType, int entityId, List<int> tagIds)
        {
            tagMapRepository.Add(entityType, entityId, tagIds);
        }
        
        public List<TagMap> Get(string entityType, int entityId)
        {
            return tagMapRepository.Get(entityType, entityId);
        }
        
        public void Remove(string entityType, int entityId)
        {
            tagMapRepository.Remove(entityType, entityId);
        }
    }
}