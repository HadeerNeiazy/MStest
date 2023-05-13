using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Identity;
using MStest.Areas.Identity.Pages.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace MStest.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public UserType UserType { get; set; }
    }
}
