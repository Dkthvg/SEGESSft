using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;

namespace SEGES.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RequirementsController : GenericController<Requirement>
    {
        private readonly IRequirementsUnitOfWork _unitOfWork;
        public RequirementsController(IGenericUnitOfWork<Requirement> unitOfWork, IRequirementsUnitOfWork requirementsUnitOfWork) : base(unitOfWork)
        {
            _unitOfWork = requirementsUnitOfWork;
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
