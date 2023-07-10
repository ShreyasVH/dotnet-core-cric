using Com.Dotnet.Cric.Exceptions;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Services
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepository;

        public MatchService(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public Match Create(CreateRequest createRequest)
        {
            createRequest.Validate();

            Match existingMatch = _matchRepository.GetByStadiumAndStartTime(createRequest.StadiumId, createRequest.StartTime.Value);
            if (null != existingMatch)
            {
                throw new ConflictException("Match");
            }

            return _matchRepository.Create(createRequest);
        }
    }
}