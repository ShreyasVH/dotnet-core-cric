using System.Collections.Generic;
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

        public List<Match> GetBySeriesId(long seriesId)
        {
            return _matchRepository.GetBySeriesId(seriesId);
        }

        public Match GetById(int id)
        {
            return _matchRepository.GetById(id);
        }
        
        public void Remove(int id)
        {
            _matchRepository.Remove(id);
        }
    }
}