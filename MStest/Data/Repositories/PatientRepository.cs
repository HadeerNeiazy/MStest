using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MStest.Data.Entities;
using MStest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Patient = MStest.Data.Entities.Patient;

namespace MStest.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MStestContext mStestContext;

        public PatientRepository(MStestContext mStestContext)
        {
            this.mStestContext = mStestContext;
        }
        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            await mStestContext.Patients.AddAsync(patient);
            await mStestContext.SaveChangesAsync();
            return await mStestContext.Patients.SingleAsync(p => p.Id == patient.Id);
        }

        public async Task<Patient> AddPatientMedicineAsync(PatientMedicine medicine, string userId)
        {
            var patient = await GetByUserIdAsync(userId);
            Medicine patientMedicine = CreateMedicine(medicine);
            if (patient != null)
            {
                patient.Medicines.Add(patientMedicine);
            }
            else
            {
                await AddPatientAsync(new Patient() { Medicines = new List<Medicine>() { patientMedicine }, ApplicationUserId = userId });
            }
            await mStestContext.SaveChangesAsync();
            return patient;
        }

        private static Medicine CreateMedicine(PatientMedicine medicine)
        {
            var patientMedicine = new Medicine()
            {
                Name = medicine.Name,
                Times = new List<MedicineTime>()
            };

            foreach (var time in medicine.Times)
            {
                patientMedicine.Times.Add(new MedicineTime() { Time = time });
            }

            return patientMedicine;
        }

        public async Task<Patient> GetByUserIdAsync(string userId)
        {
            return await mStestContext.Patients.Include(x => x.Medicines).SingleOrDefaultAsync(p => p.ApplicationUser.Id == userId);
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            // Find patient in context
            var existingPatient = await mStestContext.Patients.SingleAsync(p => p.Id == patient.Id);

            // Update patient properties
            existingPatient.Medicines = patient.Medicines;

            // Save changes to context
            await mStestContext.SaveChangesAsync();

            // Return patient
            return existingPatient;
        }

        public async Task<List<string>> CheckMedicineTime()
        {
            var medicineTimes = await mStestContext.Medicines
                               .Where(m => m.Patient.ApplicationUserId == CurrentUser.UserId)
                               .SelectMany(x => x.Times).Include(x => x.Medicine)
                               .Where(t => t.Time.Hour == DateTime.Now.Hour).ToListAsync();

            if (medicineTimes.Count > 0)
                return medicineTimes.Select(x => x.Medicine.Name).ToList();

            return null;
        }

        
        public async Task<Patient> GetByUserId(string id)
        {
            return await mStestContext.Patients.Include(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.ApplicationUserId == id);
        }

        public async Task<Patient> GetById(int patientId)
        {
            return await mStestContext.Patients.Include(x => x.ApplicationUser).FirstOrDefaultAsync(x => x.Id == patientId);
        }

        public async Task UpdateAsync(Patient pateint)
        {
            mStestContext.Patients.Update(pateint);
            await mStestContext.SaveChangesAsync();
        }
    }
}
