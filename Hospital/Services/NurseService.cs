using Hospital.DataObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class NurseService
    {
        private hospitalContext HospitalContext;

        public NurseService()
        {
            HospitalContext = new();
        }

        public IEnumerable<Nurse> GetAll()
        {
            return HospitalContext.Nurses.ToList();
        }

        public async Task<Nurse> GetById(int id)
        {
            return await HospitalContext.Nurses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Nurse nurse)
        {
            if (nurse == null)
            {
                return false;
            }

            try
            {
                HospitalContext.Nurses.Update(nurse);

                await HospitalContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        public void Delete(int id)
        {
            var toDelete = HospitalContext.Nurses.Find(id);

            if (toDelete != null)
            {
                HospitalContext.Nurses.Remove(toDelete);
                HospitalContext.SaveChanges();
            }
        }
    }
}
