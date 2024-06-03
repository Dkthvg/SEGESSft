using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories.Implementations
{
    public class GoalsRepository : GenericRepository<Goal>, IGoalsRepository
    {
        private readonly DataContext _context;
        public GoalsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Goal>>> GetAsync()
        {
            var goals = await _context.Goals
                .OrderBy(x => x.GoalName)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Goal>>
            {
                WasSuccess = true,
                Result = goals
            };
        }

        public override async Task<ActionResponse<Goal>> GetAsync(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return new ActionResponse<Goal>
                {
                    WasSuccess = false,
                    Message = "Objetivo no existe"
                };
            }
            return new ActionResponse<Goal>
            {
                WasSuccess = true,
                Result = goal
            };
        }
    }
}
