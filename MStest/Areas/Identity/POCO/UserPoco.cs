using Microsoft.AspNetCore.Http;
using MStest.Areas.Identity.Data;

namespace MStest.Areas.Identity.POCO
{
    public class UserPoco:ApplicationUser
    {
        public string Description { get; set; }
        public string ExpertIn { get; set; }
        public IFormFile ImageFile { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
