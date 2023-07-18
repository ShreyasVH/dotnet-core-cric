using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Tours;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Services
{
	public class TourService
	{
		private readonly TourRepository tourRepository;

		public TourService(TourRepository tourRepository)
		{
			this.tourRepository = tourRepository;
		}

		public Tour Create(CreateRequest createRequest)
		{
			createRequest.Validate();

			var existingTour = tourRepository.GetByNameAndStartTime(createRequest.Name, createRequest.StartTime.Value);
			if (null != existingTour)
			{
				throw new ConflictException("Tour");
			}

			return tourRepository.Create(createRequest);
		}

		public Tour GetById(long id)
		{
			return tourRepository.GetById(id);
		}
		
		public List<Tour> GetByIds(List<long> ids)
		{
			return tourRepository.GetByIds(ids);
		}
		
		public List<Tour> GetAllForYear(int year, int page, int limit)
		{
			return tourRepository.GetAllForYear(year, page, limit);
		}

		public int GetTotalCountForYear(int year)
		{
			return tourRepository.GetTotalCountForYear(year);
		}

		public List<int> GetAllYears()
		{
			return tourRepository.GetAllYears();
		}
	}
}