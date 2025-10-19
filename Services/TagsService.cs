using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Services
{
    public class TagsService
    {
        private readonly TagsRepository tagsRepository;

        public TagsService(TagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public List<Tag> GetAll(int page, int limit)
        {
            return tagsRepository.GetAll(page, limit);
        }

        public int GetTotalCount()
        {
            return tagsRepository.GetTotalCount();
        }
    }
}