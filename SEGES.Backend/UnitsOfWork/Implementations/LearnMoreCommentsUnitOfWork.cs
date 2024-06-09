using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class LearnMoreCommentsUnitOfWork : GenericUnitOfWork<LearnMoreComments>, ILearnMoreCommentsUnitOfWork
    {
        private readonly ILearnMoreCommentsRepository _repository;

        public LearnMoreCommentsUnitOfWork(IGenericRepository<LearnMoreComments> repository, ILearnMoreCommentsRepository Repository) : base(repository)
        {
            _repository = Repository;
        }
    }
}