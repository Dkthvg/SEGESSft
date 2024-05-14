using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;
using SEGES.Backend.Helpers;
using SEGES.Shared.DTOs;

namespace SEGES.Backend.Repositories.Implementations
{
    public class DocTraceabilityTypesRepository : GenericRepository<DocTraceabilityType>, IDocTraceabilityTypesRepository
    {
        private readonly DataContext _context;

        public DocTraceabilityTypesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<DocTraceabilityType>>> GetAsync()
        {
            var docTraceabilityTypes = await _context.DocTraceabilityTypes
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<DocTraceabilityType>>
            {
                WasSuccess = true,
                Result = docTraceabilityTypes
            };
        }

        public override async Task<ActionResponse<IEnumerable<DocTraceabilityType>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.DocTraceabilityTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<DocTraceabilityType>>
            {
                WasSuccess = true,
                Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.DocTraceabilityTypes.AsQueryable();

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

        public override async Task<ActionResponse<DocTraceabilityType>> GetAsync(int id)
        {
            var type = await _context.DocTraceabilityTypes.FindAsync(id);
            if (type == null)
            {
                return new ActionResponse<DocTraceabilityType>
                {
                    WasSuccess = false,
                    Message = "Tipo no existe"
                };
            }
            return new ActionResponse<DocTraceabilityType>
            {
                WasSuccess = true,
                Result = type
            };
        }
    }
}