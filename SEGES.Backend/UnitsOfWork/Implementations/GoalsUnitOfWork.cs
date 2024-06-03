using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class GoalsUnitOfWork : GenericUnitOfWork<Goal>, IGoalsUnitOfWork
    {
        private readonly IGoalsRepository _goalsRepository;

        public GoalsUnitOfWork(IGenericRepository<Goal> repository, IGoalsRepository goalsRepository) : base(repository)
        {
            _goalsRepository = goalsRepository;
        }
        
        public override async Task<ActionResponse<Goal>> GetAsync(int id) => await _goalsRepository.GetAsync(id);


        public override async Task<ActionResponse<IEnumerable<Goal>>> GetAsync() => await _goalsRepository.GetAsync();
    }
}
