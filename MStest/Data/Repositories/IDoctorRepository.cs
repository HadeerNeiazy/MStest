using MStest.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MStest.Data.Repositories
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetDoctotListAsync();
    }
}
