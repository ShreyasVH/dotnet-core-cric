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
	}
}