using Microsoft.AspNetCore.Mvc;

namespace MStest.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Doctors()
        {
            return View();
        }
        public IActionResult Diagnosis()
        {
            return View();
        }
    }
}
