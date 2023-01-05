using Microsoft.AspNetCore.Mvc;

namespace my_clinic_api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class RedirectController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger/index.html");
        }
    }
}
