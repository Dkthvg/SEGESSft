using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.UnitsOfWork.Implementations;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;

namespace SEGES.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : GenericController<UserApp>
    {
        private readonly IUsersUnitOfWork _userUnitOfWork;
        public UsersController(UnitsOfWork.Interfaces.IGenericUnitOfWork<UserApp> unitOfWork, IUsersUnitOfWork userUnitOfWork) : base(unitOfWork)
        {
            _userUnitOfWork = userUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _userUnitOfWork.GetComboAsync());
        }

        [AllowAnonymous]
        [HttpGet("email")]
        public async Task<UserApp> GetAsync(string email)
        {
            var response = await _userUnitOfWork.GetUserAsync(email);
            return response;
        }

    }
}
