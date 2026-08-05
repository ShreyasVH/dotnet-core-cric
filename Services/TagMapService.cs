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

        public void Add(int entityId, List<int> tagIds)
        {
            tagMapRepository.Add(entityId, tagIds);
        }
        
        public List<TagMap> Get(int entityId, List<int> tagIds)
        {
            return tagMapRepository.Get(entityId, tagIds);
        }
        
        public void Remove(int entityId, List<int> tagIds)
        {
            tagMapRepository.Remove(entityId, tagIds);
        }
    }
}