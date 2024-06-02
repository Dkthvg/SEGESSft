using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.UnitsOfWork.Interfaces
{
    public interface IProjectsUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Project>>> GetProjectByReqEngenieerAsync(string id);
    }
}
