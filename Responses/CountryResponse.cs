using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
	public class CountryResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }

		public CountryResponse()
		{

		}

		public CountryResponse(Country country)
		{
			this.Id = country.Id;
			this.Name = country.Name;
		}
	}
}