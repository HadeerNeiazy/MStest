using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MStest.Data.Entities;
using MStest.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task AddDoctorAsync(Doctor doctor)
        {
            await mStestContext.Doctors.AddAsync(doctor);
            await mStestContext.SaveChangesAsync();
        }

        public string DiagnoseAsync(List<IFormFile> documents)
        {
            var client = new RestClient("http://127.0.0.1:5000");

            var request = new RestRequest("/predict",Method.Post);

            MemoryStream stream = new MemoryStream();
            documents[0].CopyTo(stream);
            request.AddFile("my_image",bytes: stream.ToArray(), documents[0].FileName);

            var response = client.Execute(request);
            return response.Content;
        }

        public async Task<Doctor> GetById(int doctorId)
        {
            return await mStestContext.Doctors.Include(x => x.ApplicationUser).FirstAsync(x => x.Id == doctorId);
        }

        public async Task<Doctor> GetByUserId(string id)
        {
            return await mStestContext.Doctors.Include(x=>x.ApplicationUser).FirstOrDefaultAsync(x => x.ApplicationUserId == id);
        }

        public Task<List<Doctor>> GetDoctotListAsync()
        {
            return mStestContext.Doctors.Include(x => x.ApplicationUser).ToListAsync();
        }
        public async Task UpdateAsync(Doctor doctor)
        {
            mStestContext.Doctors.Update(doctor);
            await mStestContext.SaveChangesAsync();
        }
    }
}
