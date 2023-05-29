using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
	public class StadiumResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public CountryResponse Country { get; set; }

		public StadiumResponse()
		{

		}

		public StadiumResponse(Stadium stadium, CountryResponse countryResponse)
		{
			this.Id = stadium.Id;
			this.Name = stadium.Name;
			this.City = stadium.City;
			this.State = stadium.State;
			this.Country = countryResponse;
		}
	}
}