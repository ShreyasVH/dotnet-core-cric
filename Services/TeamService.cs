using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Teams;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Services
{
	public class TeamService
	{
		private readonly TeamRepository teamRepository;

		public TeamService(TeamRepository teamRepository)
		{
			this.teamRepository = teamRepository;
		}

		public Team Create(CreateRequest createRequest)
		{
			createRequest.Validate();

			var existingTeam = teamRepository.GetByNameAndCountryIdAndTypeId(createRequest.Name, createRequest.CountryId, createRequest.TypeId);
			if (null != existingTeam)
			{
				throw new ConflictException("Team");
			}

			return teamRepository.Create(createRequest);
		}

		public List<Team> GetAll(int page, int limit)
		{
			return teamRepository.GetAll(page, limit);
		}

		public int GetTotalCount()
		{
			return teamRepository.GetTotalCount();
		}
	}
}