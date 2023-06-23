using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MStest.Areas.Identity.Data;
using MStest.Areas.Identity.ViewModels;
using MStest.Data.Entities;
using MStest.Data.Repositories;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace MStest.Areas.Identity.Pages.Account
{
    public class EditProfileModel : PageModel
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;
        public UserViewModel userPoco;

        public EditProfileModel(IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            this.httpContext = httpContext;
            this.userManager = userManager;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }
        public async Task OnGet()
        {
            var user = await userManager.GetUserAsync(httpContext.HttpContext.User);
            userPoco = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = user.UserType,
                Id = user.Id,
                Image = user.Image,
            };
            if (user.UserType == UserType.Doctor)
            {
                var doctor = await doctorRepository.GetByUserId(user.Id);
                if (doctor != null)
                {
                    userPoco.ExpertIn = doctor.ExpertIn;
                    userPoco.Description = doctor.Description;
                    userPoco.DoctorId = doctor.Id;
                }
            }
            else if (user.UserType == UserType.Patient)
            {
                var pateint = await patientRepository.GetByUserId(user.Id);
                if (pateint != null)
                {
                    userPoco.PatientId = pateint.Id;
                }

            }
        }
        public async Task OnPost([FromForm] UserViewModel userPoco)
        {
            //save image
            if (userPoco.ImageFile != null)
            {
                var fileName = Path.GetFileName(userPoco.ImageFile.FileName);
                userPoco.Image = fileName;
                using (var stream = new FileStream($"./wwwroot/img/{userPoco.Image}", FileMode.Create))
                {
                    await userPoco.ImageFile.CopyToAsync(stream);
                }
            }

            if (userPoco.UserType == UserType.Patient)
            {
                if (userPoco.PatientId == 0)
                {
                    var user = await userManager.FindByIdAsync(userPoco.Id);
                    user.Image = userPoco.Image;
                    await userManager.UpdateAsync(user);

                    var patient = new Patient(userPoco.Id);
                    await patientRepository.AddPatientAsync(patient);
                }
                else
                {
                    var user = await userManager.FindByIdAsync(userPoco.Id);
                    user.Image = userPoco.Image;
                    await userManager.UpdateAsync(user);

                    var pateint = await patientRepository.GetById(userPoco.PatientId);
                    pateint.ApplicationUser.Image = userPoco.Image;
                    pateint.ApplicationUser.FirstName = userPoco.FirstName;
                    pateint.ApplicationUser.LastName = userPoco.LastName;
                    pateint.ApplicationUser.Email = userPoco.Email;
                    pateint.ApplicationUser.UserType = userPoco.UserType;

                    await patientRepository.UpdateAsync(pateint);
                }

            }
            else if (userPoco.UserType == UserType.Doctor)
            {

                if (userPoco.DoctorId == 0)
                {
                    var user = await userManager.FindByIdAsync(userPoco.Id);
                    user.Image = userPoco.Image;
                    await userManager.UpdateAsync(user);

                    var doctor = new Doctor(userPoco.Description, userPoco.ExpertIn, userPoco.Id);
                    await doctorRepository.AddDoctorAsync(doctor);
                }
                else
                {
                    var doctor = await doctorRepository.GetById(userPoco.DoctorId);
                    doctor.ApplicationUser.Image = userPoco.Image;
                    doctor.ApplicationUser.FirstName = userPoco.FirstName;
                    doctor.ApplicationUser.LastName = userPoco.LastName;
                    doctor.ApplicationUser.Email = userPoco.Email;
                    doctor.ApplicationUser.UserType = userPoco.UserType;
                    doctor.ExpertIn = userPoco.ExpertIn;
                    doctor.Description = userPoco.Description;
                    await doctorRepository.UpdateAsync(doctor);
                }
            }

            await OnGet();
        }
    }
}
