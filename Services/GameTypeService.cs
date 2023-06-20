using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Services
{
    public class GameTypeService
    {
        private readonly GameTypeRepository gameTypeRepository;

        public GameTypeService(GameTypeRepository gameTypeRepository)
        {
            this.gameTypeRepository = gameTypeRepository;
        }

        public GameType FindById(int id)
        {
            return gameTypeRepository.GetById(id);
        }
    }
}