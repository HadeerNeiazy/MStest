using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MStest.Areas.Identity.Data;
using MStest.Data.Repositories;
using MStest.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MStest.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IPatientRepository patientRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public PatientController(IPatientRepository patientRepository, UserManager<ApplicationUser> userManager)
        {
            this.patientRepository = patientRepository;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMedicine()
        {
            CurrentUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicine([FromForm] PatientMedicine patientMedicine)
        {
            CurrentUser.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await patientRepository.AddPatientMedicineAsync(patientMedicine, CurrentUser.UserId);
            return View();
        }

        [HttpGet]
        public async Task<List<string>> CheckMedicineTime()
        {
             return await patientRepository.CheckMedicineTime();
        }
    }
}
