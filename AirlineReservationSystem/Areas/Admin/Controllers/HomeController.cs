using AirlineReservationSystem.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Areas.Admin.Controllers
{
    [Authorize(Roles =$"{UserConstants.Role.AdministratorRole},{UserConstants.Role.FleetManagerRole},{UserConstants.Role.FlightManagerRole}")]
    [Route("Admin/Home")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var User = this.User;
            return View();
        }
    }
}
