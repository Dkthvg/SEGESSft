using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using System.Threading.Tasks;

namespace SEGES.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HUPrioritiesController : GenericController<HUPriority>
    {
        private readonly IHUPrioritiesUnitOfWork _huPrioritiesUnitOfWork;
        public HUPrioritiesController(IGenericUnitOfWork<HUPriority> unitOfWork, IHUPrioritiesUnitOfWork huPrioritiesUnitOfWork) : base(unitOfWork)
        {
            _huPrioritiesUnitOfWork = huPrioritiesUnitOfWork;
        }
        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _huPrioritiesUnitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _huPrioritiesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _huPrioritiesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _huPrioritiesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

    }
}
