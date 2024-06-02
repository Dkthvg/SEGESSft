using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Implementations
{
    public class ProjectsRepository : GenericRepository<Project>, IProjectsRepository
    {
        private readonly DataContext _context;
        public ProjectsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ActionResponse<IEnumerable<Project>>> GetProjectByReqEngenieerAsync(string id)
        {
            var projects = await _context.Projects
                .Where(x => x.RequirementsEngineer_ID == id)
                .OrderBy(x => x.CreationDate)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Project>>
            {
                WasSuccess = true,
                Result = projects
            };
        }
    }
}
