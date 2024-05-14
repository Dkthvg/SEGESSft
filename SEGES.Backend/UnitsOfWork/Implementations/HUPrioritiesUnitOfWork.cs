using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class HUPrioritiesUnitOfWork : GenericUnitOfWork<HUPriority>, IHUPrioritiesUnitOfWork
    {
        private readonly IHUPrioritiesRepository _huRepository;
        public HUPrioritiesUnitOfWork(IGenericRepository<HUPriority> repository, IHUPrioritiesRepository huRepository) : base(repository)
        {
            _huRepository = huRepository;
        }
        public override async Task<ActionResponse<HUPriority>> GetAsync(int id) => await _huRepository.GetAsync(id);
        public override async Task<ActionResponse<IEnumerable<HUPriority>>> GetAsync() => await _huRepository.GetAsync();
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _huRepository.GetTotalPagesAsync(pagination);
        public override async Task<ActionResponse<IEnumerable<HUPriority>>> GetAsync(PaginationDTO pagination) => await _huRepository.GetAsync(pagination);
    }
}
