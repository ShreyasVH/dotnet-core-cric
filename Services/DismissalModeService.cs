using System.Collections.Generic;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Repositories;

namespace Com.Dotnet.Cric.Services
{
    public class DismissalModeService
    {
        private readonly DismissalModeRepository _dismissalModeRepository;

        public DismissalModeService(DismissalModeRepository dismissalModeRepository)
        {
            _dismissalModeRepository = dismissalModeRepository;
        }

        public List<DismissalMode> GetAll()
        {
            return _dismissalModeRepository.GetAll();
        }
    }
}