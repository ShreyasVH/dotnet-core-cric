using System;
using System.Collections.Generic;
using System.Text.Json;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class TotalsService
    {
        private readonly TotalsRepository _totalsRepository;

        public TotalsService(TotalsRepository totalsRepository)
        {
            _totalsRepository = totalsRepository;
        }

        public void Add(List<Total> totals)
        {
            _totalsRepository.Add(totals);
        }
        
        public void Remove(int matchId)
        {
            _totalsRepository.Remove(matchId);
        }
    }
}