using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class ProjectStatusesUnitOfWork : GenericUnitOfWork<ProjectStatus>, IProjectStatusesUnitOfWork
    {
        private readonly IProjectStatusesRepository _projectStatusesRepository;
        public ProjectStatusesUnitOfWork(IGenericRepository<ProjectStatus> repository, IProjectStatusesRepository projectStatusesRepository) : base(repository)
        {
            _projectStatusesRepository = projectStatusesRepository;
        }
        public override async Task<ActionResponse<ProjectStatus>> GetAsync(int id) => await _projectStatusesRepository.GetAsync(id);


        public override async Task<ActionResponse<IEnumerable<ProjectStatus>>> GetAsync() => await _projectStatusesRepository.GetAsync();
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _projectStatusesRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<ProjectStatus>>> GetAsync(PaginationDTO pagination) => await _projectStatusesRepository.GetAsync(pagination);

        public async Task<IEnumerable<ProjectStatus>> GetComboAsync() => await _projectStatusesRepository.GetComboAsync();
    }
}
