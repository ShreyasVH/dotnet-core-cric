using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Countries;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Services
{
	public class CountryService
	{
		private readonly CountryRepository countryRepository;

		public CountryService(CountryRepository countryRepository)
		{
			this.countryRepository = countryRepository;
		}

		public Country Create(CreateRequest createRequest)
		{
			createRequest.Validate();

			var existingCountry = countryRepository.GetByName(createRequest.Name);
			if(null != existingCountry)
			{
				throw new ConflictException("Country");
			}

			return countryRepository.Create(createRequest);
		}

		public List<Country> SearchByName(string name)
		{
			return countryRepository.GetByNamePattern(name);
		}
	}
}