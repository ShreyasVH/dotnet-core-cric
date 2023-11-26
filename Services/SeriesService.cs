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

        public Series GetById(long id)
        {
            return seriesRepository.GetById(id);
        }

        public Series Update(Series existingSeries, UpdateRequest updateRequest)
        {
            var isUpdateRequired = false;

            if (!string.IsNullOrEmpty(updateRequest.Name) && !updateRequest.Name.Equals(existingSeries.Name))
            {
                isUpdateRequired = true;
                existingSeries.Name = updateRequest.Name;
            }
            
            if(updateRequest.HomeCountryId.HasValue && updateRequest.HomeCountryId.Value != existingSeries.HomeCountryId)
            {
                isUpdateRequired = true;
                existingSeries.HomeCountryId = updateRequest.HomeCountryId.Value;
            }
            
            if(updateRequest.TourId.HasValue && updateRequest.TourId.Value != existingSeries.TourId)
            {
                isUpdateRequired = true;
                existingSeries.TourId = updateRequest.TourId.Value;
            }
            
            if(updateRequest.TypeId.HasValue && updateRequest.TypeId.Value != existingSeries.TypeId)
            {
                isUpdateRequired = true;
                existingSeries.TypeId = updateRequest.TypeId.Value;
            }
            
            if(updateRequest.GameTypeId.HasValue && updateRequest.GameTypeId.Value != existingSeries.GameTypeId)
            {
                isUpdateRequired = true;
                existingSeries.GameTypeId = updateRequest.GameTypeId.Value;
            }

            if (updateRequest.StartTime.HasValue && updateRequest.StartTime.Value != existingSeries.StartTime)
            {
                isUpdateRequired = true;
                existingSeries.StartTime = updateRequest.StartTime.Value;
            }
            
            if (isUpdateRequired)
            {
                seriesRepository.Update(existingSeries);
            }

            return existingSeries;
        }

        public List<Series> GetByTourId(long tourId)
        {
            return seriesRepository.GetByTourId(tourId);
        }

        public void Remove(long id)
        {
            seriesRepository.Remove(id);
        }
    }
}