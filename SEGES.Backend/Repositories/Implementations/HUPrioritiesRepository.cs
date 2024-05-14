using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.Helpers;
using SEGES.Shared.DTOs;

namespace SEGES.Backend.Repositories.Implementations
{
    public class HUPrioritiesRepository : GenericRepository<HUPriority>, IHUPrioritiesRepository
    {
        private readonly DataContext _context;

        public HUPrioritiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<HUPriority>> GetAsync(int id)
        {
            var priority = await _context.HUPriorities.FindAsync(id);
            if (priority == null)
            {
                return new ActionResponse<HUPriority>
                {
                    WasSuccess = false,
                    Message = "Prioridad no existe"
                };
            }
            return new ActionResponse<HUPriority>
            {
                WasSuccess = true,
                Result = priority
            };
        }

        public override async Task<ActionResponse<IEnumerable<HUPriority>>> GetAsync()
        {
            var HUPriority = await _context.HUPriorities
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<HUPriority>>
            {
                WasSuccess = true,
                Result = HUPriority
            };
        }

        public override async Task<ActionResponse<IEnumerable<HUPriority>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.HUPriorities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<HUPriority>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.HUPriorities.AsQueryable();

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
    }
}