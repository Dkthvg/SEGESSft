using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;

namespace SEGES.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LearnMoreCommentsController : GenericController<LearnMoreComments>
    {
        private readonly ILearnMoreCommentsUnitOfWork _unitOfWork;

        public LearnMoreCommentsController(IGenericUnitOfWork<LearnMoreComments> unitOfWork, ILearnMoreCommentsUnitOfWork ILearnCommentsUnitOfWork) : base(unitOfWork)
        {
            _unitOfWork = ILearnCommentsUnitOfWork;
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _unitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _unitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }
    }
}