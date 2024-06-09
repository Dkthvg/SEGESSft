using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Interfaces
{
    public interface ILearnMoreCommentsRepository
    {
        Task<ActionResponse<LearnMoreComments>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<LearnMoreComments>>> GetAsync();
    }
}