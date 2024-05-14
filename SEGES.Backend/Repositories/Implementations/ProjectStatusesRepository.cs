using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Helpers;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Implementations
{
    public class ProjectStatusesRepository : GenericRepository<ProjectStatus>, IProjectStatusesRepository
    {
        private readonly DataContext _context;

        public ProjectStatusesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<ProjectStatus>>> GetAsync()
        {
            var projectStatuses = await _context.ProjectStatuses
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<ProjectStatus>>
            {
                WasSuccess = true,
                Result = projectStatuses
            };
        }

        public override async Task<ActionResponse<IEnumerable<ProjectStatus>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.ProjectStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<ProjectStatus>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.ProjectStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<ProjectStatus>> GetAsync(int id)
        {
            var type = await _context.ProjectStatuses.FindAsync(id);
            if (type == null)
            {
                return new ActionResponse<ProjectStatus>
                {
                    WasSuccess = false,
                    Message = "Tipo no existe"
                };
            }
            return new ActionResponse<ProjectStatus>
            {
                WasSuccess = true,
                Result = type
            };
        }
    }
}