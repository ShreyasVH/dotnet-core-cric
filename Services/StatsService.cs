using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Countries;
using Com.Dotnet.Cric.Exceptions;
using Com.Dotnet.Cric.Responses;
using dotnet.Requests;

namespace Com.Dotnet.Cric.Services
{
    public class StatsService
    {
        private readonly PlayerRepository _playerRepository;

        public StatsService(PlayerRepository playerRepository)
        {
            this._playerRepository = playerRepository;
        }

        public StatsResponse GetStats(FilterRequest filterRequest)
        {
            var statsResponse = new StatsResponse();
            if ("batting" == filterRequest.Type)
            {
                statsResponse = _playerRepository.GetBattingStats(filterRequest);
            }
            else if ("bowling" == filterRequest.Type)
            {
                statsResponse = _playerRepository.GetBowlingStats(filterRequest);
            }
            else if ("fielding" == filterRequest.Type)
            {
                statsResponse = _playerRepository.GetFieldingStats(filterRequest);
            }

            return statsResponse;
        }
    }
}