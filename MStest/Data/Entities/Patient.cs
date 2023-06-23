using Microsoft.AspNetCore.Builder;
using MStest.Areas.Identity.Data;
using MStest.Models;
using System.Collections.Generic;
using System.Security.Permissions;

namespace MStest.Data.Entities
{
    public class Patient
    {
        public Patient()
        {

        }
        public Patient(string applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }

        public int Id { get; set; }
        public List<Medicine> Medicines { get; set; }
        public string ApplicationUserId { get; set; }
        public string Image { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
