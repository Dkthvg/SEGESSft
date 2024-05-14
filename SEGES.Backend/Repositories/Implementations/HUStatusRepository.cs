using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Helpers;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Implementations
{
    public class HUStatusRepository : GenericRepository<HUStatus>, IHUStatusRepository
    {
        private readonly DataContext _context;

        public HUStatusRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<HUStatus>>> GetAsync()
        {
            var huStatus = await _context.HUStatuses
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<HUStatus>>
            {
                WasSuccess = true,
                Result = huStatus
            };
        }

        public async Task<IEnumerable<HUStatus>> GetComboAsync()
        {
            return await _context.HUStatuses
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<IEnumerable<HUStatus>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.HUStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<HUStatus>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.HUStatuses.AsQueryable();

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

        public override async Task<ActionResponse<HUStatus>> GetAsync(int id)
        {
            var type = await _context.HUStatuses.FindAsync(id);
            if (type == null)
            {
                return new ActionResponse<HUStatus>
                {
                    WasSuccess = false,
                    Message = "Tipo no existe"
                };
            }
            return new ActionResponse<HUStatus>
            {
                WasSuccess = true,
                Result = type
            };
        }
    }
}