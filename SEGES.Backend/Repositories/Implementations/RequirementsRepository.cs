using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Implementations
{
    public class RequirementsRepository : GenericRepository<Requirement>, IRequirementsRepository
    {
        private readonly DataContext _context;
        public RequirementsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Requirement>>> GetAsync()
        {
            var requirements = await _context.Requirements
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Requirement>>
            {
                WasSuccess = true,
                Result = requirements
            };
        }

        public override async Task<ActionResponse<Requirement>> GetAsync(int id)
        {
            var req = await _context.Requirements.FindAsync(id);
            if (req == null)
            {
                return new ActionResponse<Requirement>
                {
                    WasSuccess = false,
                    Message = "Requerimiento no existe"
                };
            }
            return new ActionResponse<Requirement>
            {
                WasSuccess = true,
                Result = req
            };
        }
    }
}
