using MStest.Areas.Identity.Data;
using System;

namespace MStest.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string Image { get; set; }
        public string ExpertIn { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
