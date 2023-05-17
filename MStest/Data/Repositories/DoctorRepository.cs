using Microsoft.EntityFrameworkCore;
using MStest.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MStest.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MStestContext mStestContext;

        public DoctorRepository(MStestContext mStestContext)
        {
            this.mStestContext = mStestContext;
        }
        public Task<List<Doctor>> GetDoctotListAsync()
        {
            return mStestContext.Doctors.Include(x=>x.ApplicationUser).ToListAsync();
        }
    }
}
