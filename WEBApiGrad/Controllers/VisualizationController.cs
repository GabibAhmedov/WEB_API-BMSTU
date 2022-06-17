using Microsoft.AspNetCore.Mvc;

namespace WEBApiGrad.Controllers
{
    [Route("api/[controller]")]
    public class VisualizationController : Controller
    {
        [HttpGet]
        [Route("chart")]
        public IActionResult Draw()
        {
            return View();
        }
    }
}
