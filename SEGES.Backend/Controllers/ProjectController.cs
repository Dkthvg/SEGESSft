using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;

namespace SEGES.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectController : GenericController<Project>
    {
        public ProjectController(IGenericUnitOfWork<Project> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
