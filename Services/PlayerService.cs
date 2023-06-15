using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Players;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Services
{
    public class PlayerService
    {
        private readonly PlayerRepository playerRepository;

        public PlayerService(PlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public Player Create(CreateRequest createRequest)
        {
            createRequest.Validate();

            var existingPlayer = playerRepository.GetByNameAndCountryIdAndDateOfBirth(createRequest.Name, createRequest.CountryId, createRequest.DateOfBirth.Value);
            if (null != existingPlayer)
            {
                throw new ConflictException("Player");
            }

            return playerRepository.Create(createRequest);
        }
    }
}