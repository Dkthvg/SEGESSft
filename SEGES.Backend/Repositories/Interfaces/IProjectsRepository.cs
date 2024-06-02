using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Interfaces
{
    public interface IProjectsRepository
    {
        Task<ActionResponse<IEnumerable<Project>>> GetProjectByReqEngenieerAsync(string id);
    }
}
