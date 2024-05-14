using Microsoft.AspNetCore.Mvc;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.Entities;

namespace SEGES.Backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HUPublicationStatusController : GenericController<HUPublicationStatus>
    {
        public HUPublicationStatusController(IGenericUnitOfWork<HUPublicationStatus> unitOfWork) : base(unitOfWork)
        {
        }
    }
}

