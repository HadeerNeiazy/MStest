using MStest.Areas.Identity.Data;
using System;

namespace MStest.Data.Entities
{
    public class Doctor
    {
        public Doctor(string description, string expertIn, string applicationUserId)
        {
            Description = description;
            ExpertIn = expertIn;
            ApplicationUserId =applicationUserId;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string ExpertIn { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
