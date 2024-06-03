using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class RequirementsUnitOfWork : GenericUnitOfWork<Requirement>, IRequirementsUnitOfWork
    {
        private readonly IRequirementsRepository _requirementsRepository;

        public RequirementsUnitOfWork(IGenericRepository<Requirement> repository, IRequirementsRepository requirementsRepository):base(repository) 
        {
            _requirementsRepository = requirementsRepository;
        }
        public override async Task<ActionResponse<Requirement>> GetAsync(int id) => await _requirementsRepository.GetAsync(id);


        public override async Task<ActionResponse<IEnumerable<Requirement>>> GetAsync() => await _requirementsRepository.GetAsync();
    }
}
