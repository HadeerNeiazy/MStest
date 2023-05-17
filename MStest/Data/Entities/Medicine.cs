using System;
using System.Collections.Generic;

namespace MStest.Data.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MedicineTime> Times { get; set; }
        public Patient  Patient { get; set; }
    }
}