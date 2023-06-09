using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MStest.Areas.Identity.Data;
using MStest.Areas.Identity.POCO;
using MStest.Data.Repositories;
using System.Threading.Tasks;

namespace MStest.Areas.Identity.Pages.Account
{
    public class ProfileDetailsModel : PageModel
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;
        public UserPoco userPoco;

        public ProfileDetailsModel(IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            this.httpContext = httpContext;
            this.userManager = userManager;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }
        public async Task OnGet()
        {
            var user = await userManager.GetUserAsync(httpContext.HttpContext.User);
            userPoco = new UserPoco()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = user.UserType,
                Id = user.Id,
            };
            if (user.UserType == UserType.Doctor)
            {
                var doctor = await doctorRepository.GetByUserId(user.Id);
                if(doctor != null)
                {
                    userPoco.ExpertIn = doctor.ExpertIn;
                    userPoco.Description = doctor.Description;
                    userPoco.Image = doctor.ApplicationUser.Image;
                }
            }
            else if(user.UserType == UserType.Patient)
            {
                var pateint = await patientRepository.GetByUserId(user.Id);
                if (pateint != null)
                {
                    userPoco.PatientId = pateint.Id;
                    userPoco.Image = pateint.Image;
                }
            }
        }
    }
}
