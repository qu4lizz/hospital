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
    public class ManagerService
    {
        private hospitalContext HospitalContext;

        public ManagerService()
        {
            HospitalContext = new();
        }

        public IEnumerable<Manager> GetAll()
        {
            return HospitalContext.Managers.ToList();
        }

        public async Task<Manager> GetById(int id)
        {
            return await HospitalContext.Managers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Manager manager)
        {
            if (manager == null)
            {
                return false;
            }

            try
            {
                HospitalContext.Managers.Update(manager);

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
            var toDelete = HospitalContext.Managers.Find(id);

            if (toDelete != null)
            {
                HospitalContext.Managers.Remove(toDelete);
                HospitalContext.SaveChanges();
            }
        }
    }
}
