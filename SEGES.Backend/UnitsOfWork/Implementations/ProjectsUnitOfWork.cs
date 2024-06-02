using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class ProjectsUnitOfWork: GenericUnitOfWork<Project>, IProjectsUnitOfWork
    {
        private readonly IProjectsRepository _projectsRepository;
        public ProjectsUnitOfWork(IGenericRepository<Project> repository, IProjectsRepository projectsRepository) : base(repository)
        {
            _projectsRepository= projectsRepository;
        }

        public  async Task<ActionResponse<IEnumerable<Project>>> GetProjectByReqEngenieerAsync(string id) => await _projectsRepository.GetProjectByReqEngenieerAsync(id);
    }
}
