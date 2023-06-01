using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
	public class TeamResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public CountryResponse Country { get; set; }
		public TeamTypeResponse Type { get; set; }

		public TeamResponse()
		{

		}

		public TeamResponse(Team team, CountryResponse countryResponse, TeamTypeResponse teamTypeResponse)
		{
			this.Id = team.Id;
			this.Name = team.Name;
			this.Country = countryResponse;
			this.Type = teamTypeResponse;
		}
	}
}