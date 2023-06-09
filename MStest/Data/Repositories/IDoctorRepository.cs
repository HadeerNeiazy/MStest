using MStest.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MStest.Data.Repositories
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor doctor);
        Task<Doctor> GetById(int patientId);
        Task<Doctor> GetByUserId(string id);
        Task<List<Doctor>> GetDoctotListAsync();
        Task UpdateAsync(Doctor doctor);
    }
}
