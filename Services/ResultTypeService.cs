using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Services
{
    public class ResultTypeService
    {
        private readonly ResultTypeRepository _resultTypeRepository;

        public ResultTypeService(ResultTypeRepository resultTypeRepository)
        {
            _resultTypeRepository = resultTypeRepository;
        }

        public ResultType FindById(int id)
        {
            return _resultTypeRepository.GetById(id);
        }
    }
}