using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;
using SEGES.Shared;
using SEGES.Backend.Helpers;
using SEGES.Shared.DTOs;

namespace SEGES.Backend.Repositories.Implementations
{
    public class HuAppruvalStatusRepository : GenericRepository<HUApprovalStatus>, IHuAppruvalStatusRepository
    {
        private readonly DataContext _context;

        public HuAppruvalStatusRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<HUApprovalStatus>>> GetAsync()
        {
            var huApprovalStatuses = await _context.HUApprovalStatuses
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<HUApprovalStatus>>
            {
                WasSuccess = true,
                Result = huApprovalStatuses
            };
        }

        public override async Task<ActionResponse<IEnumerable<HUApprovalStatus>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.HUApprovalStatuses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<HUApprovalStatus>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.HUApprovalStatuses.AsQueryable();

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

        public async Task<IEnumerable<HUApprovalStatus>> GetComboAsync()
        {
            return await _context.HUApprovalStatuses
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}