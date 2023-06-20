using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Series;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Services
{
    public class SeriesService
    {
        private readonly SeriesRepository seriesRepository;

        public SeriesService(SeriesRepository seriesRepository)
        {
            this.seriesRepository = seriesRepository;
        }

        public Series Create(CreateRequest createRequest)
        {
            createRequest.Validate();

            var existingSeries = seriesRepository.GetByNameAndTourIdAndGameTypeId(createRequest.Name, createRequest.TourId, createRequest.GameTypeId);
            if (null != existingSeries)
            {
                throw new ConflictException("Series");
            }

            return seriesRepository.Create(createRequest);
        }
        
        public List<Series> GetAll(int page, int limit)
        {
            return seriesRepository.GetAll(page, limit);
        }

        public int GetTotalCount()
        {
            return seriesRepository.GetTotalCount();
        }
    }
}