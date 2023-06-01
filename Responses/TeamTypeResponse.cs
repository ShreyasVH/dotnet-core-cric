using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
	public class TeamTypeResponse
	{
		public long Id { get; set; }
		public string Name { get; set; }

		public TeamTypeResponse()
		{

		}

		public TeamTypeResponse(TeamType teamType)
		{
			this.Id = teamType.Id;
			this.Name = teamType.Name;
		}
	}
}