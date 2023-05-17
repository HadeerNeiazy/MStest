using Microsoft.AspNetCore.Builder;
using MStest.Areas.Identity.Data;
using MStest.Models;
using System.Collections.Generic;

namespace MStest.Data.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public List<Medicine> Medicines { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
