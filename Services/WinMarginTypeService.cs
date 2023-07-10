using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Services
{
    public class WinMarginTypeService
    {
        private readonly WinMarginTypeRepository _winMarginTypeRepository;

        public WinMarginTypeService(WinMarginTypeRepository winMarginTypeRepository)
        {
            _winMarginTypeRepository = winMarginTypeRepository;
        }

        public WinMarginType FindById(int id)
        {
            return _winMarginTypeRepository.GetById(id);
        }
    }
}