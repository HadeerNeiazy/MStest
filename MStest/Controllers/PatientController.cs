using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MStest.Areas.Identity.Data;
using MStest.Data.Repositories;
using MStest.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

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
            var medicines = await patientRepository.CheckMedicineTime();
            //if (medicines.Count > 0)
            //    SendEmail(User.FindFirstValue(ClaimTypes.Name), medicines);
            return medicines;
        }
        private void SendEmail(string emailTo, List<string> medicines)
        {
            // Create a MailMessage object.
            MailMessage message = new MailMessage();

            // Set the sender and recipient addresses.
            message.From = new MailAddress("hadeerneiazy64@gmail.com");
            message.To.Add(emailTo);

            // Set the subject and body of the email.
            message.Subject = "Medidcine reminder";
            message.Body = $"this email to reminder you for the your medicnes:{string.Join(",", medicines)}";

            // Send the email.
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("hadeerneiazy64@gmail.com", "xwpas1234");
            client.EnableSsl = true;
            client.Send(message);
        }

    }
}
