using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using System.Threading.Tasks;

namespace SEGES.Backend.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectStatusesController : GenericController<ProjectStatus>
    {
        private readonly IProjectStatusesUnitOfWork _projectStatusesUnitOfWork;
        public ProjectStatusesController(IGenericUnitOfWork<ProjectStatus> unitOfWork, IProjectStatusesUnitOfWork projectStatusesUnitOfWork) : base(unitOfWork)
        {
            _projectStatusesUnitOfWork = projectStatusesUnitOfWork;
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _projectStatusesUnitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }


        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _projectStatusesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _projectStatusesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _projectStatusesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _projectStatusesUnitOfWork.GetComboAsync());
        }

    }
}
