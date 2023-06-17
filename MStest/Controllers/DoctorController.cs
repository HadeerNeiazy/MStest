using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MStest.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MStest.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Doctors()
        {
            return View(await doctorRepository.GetDoctotListAsync());
        }

        [HttpGet]
        public IActionResult Diagnosis()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Diagnosis(List<IFormFile> Documents)
        {
            var result = doctorRepository.DiagnoseAsync(Documents);
            ViewBag.DiagnosisResult=result;
            return View();
        }
    }
}
