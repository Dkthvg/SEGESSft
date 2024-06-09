using Microsoft.EntityFrameworkCore;
using SEGES.Backend.Repositories.Implementations;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;

namespace SEGES.Backend.Repositories
{
    public class LearnMoreCommentsRepository : GenericRepository<LearnMoreComments>, ILearnMoreCommentsRepository
    {
        private readonly DataContext _context;

        public LearnMoreCommentsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<LearnMoreComments>>> GetAsync()
        {
            var huStatus = await _context.LearnMoreComments
                .OrderBy(x => x.CreatedBy)
                .ToListAsync();
            return new ActionResponse<IEnumerable<LearnMoreComments>>
            {
                WasSuccess = true,
                Result = huStatus
            };
        }

        public override async Task<ActionResponse<LearnMoreComments>> GetAsync(int id)
        {
            var type = await _context.LearnMoreComments.FindAsync(id);
            if (type == null)
            {
                return new ActionResponse<LearnMoreComments>
                {
                    WasSuccess = false,
                    Message = "Comentario no existe"
                };
            }
            return new ActionResponse<LearnMoreComments>
            {
                WasSuccess = true,
                Result = type
            };
        }
    }
}