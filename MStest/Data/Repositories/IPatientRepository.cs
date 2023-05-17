using MStest.Data.Entities;
using MStest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Patient = MStest.Data.Entities.Patient;

namespace MStest.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> UpdatePatientAsync(Patient patient);
        Task<Patient> GetByUserIdAsync(string userId);
        Task<Patient> AddPatientMedicineAsync(PatientMedicine medicine,string userId);
        Task<List<string>> CheckMedicineTime();
    }
}
