using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Services
{
    public class SeriesTypeService
    {
        private readonly SeriesTypeRepository seriesTypeRepository;

        public SeriesTypeService(SeriesTypeRepository seriesTypeRepository)
        {
            this.seriesTypeRepository = seriesTypeRepository;
        }

        public SeriesType FindById(int id)
        {
            return seriesTypeRepository.GetById(id);
        }
    }
}