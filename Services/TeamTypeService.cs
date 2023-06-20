using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Services
{
	public class TeamTypeService
	{
		private readonly TeamTypeRepository teamTypeRepository;

		public TeamTypeService(TeamTypeRepository teamTypeRepository)
		{
			this.teamTypeRepository = teamTypeRepository;
		}

		public TeamType FindById(int id)
		{
			return teamTypeRepository.GetById(id);
		}

		public List<TeamType> FindByIds(List<int> ids)
		{
			return teamTypeRepository.GetByIds(ids);
		}
	}
}