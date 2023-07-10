using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class ExtrasTypeService
    {
        private readonly ExtrasTypeRepository _extrasTypeRepository;

        public ExtrasTypeService(ExtrasTypeRepository extrasTypeRepository)
        {
            _extrasTypeRepository = extrasTypeRepository;
        }

        public List<ExtrasType> GetAll()
        {
            return _extrasTypeRepository.GetAll();
        }
    }
}