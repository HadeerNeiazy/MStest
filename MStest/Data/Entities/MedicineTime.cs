using System;

namespace MStest.Data.Entities
{
    public class MedicineTime
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public Medicine Medicine { get; set; }
    }
}