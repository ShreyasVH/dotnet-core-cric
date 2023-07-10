using System.Collections.Generic;
using Com.Dotnet.Cric.Data;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Services
{
    public class BowlingFigureService
    {
        private readonly BowlingFigureRepository _bowlingFigureRepository;

        public BowlingFigureService(BowlingFigureRepository bowlingFigureRepository)
        {
            _bowlingFigureRepository = bowlingFigureRepository;
        }

        public List<BowlingFigure> Add(List<BowlingFigureRequest> bowlingFigureRequests, Dictionary<long, int> playerToMatchPlayerMap)
        {
            return _bowlingFigureRepository.Add(bowlingFigureRequests, playerToMatchPlayerMap);
        }

        public Dictionary<string, Dictionary<string, int>> GetBowlingStats(long playerId)
        {
            return _bowlingFigureRepository.GetBowlingStats(playerId);
        }
    }
}