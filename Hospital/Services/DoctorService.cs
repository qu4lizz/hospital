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
    public class DoctorService
    {
        private hospitalContext _context;

        public DoctorService()
        {
            _context = new();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Doctor GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> Update(Doctor doctor)
        {
            if (doctor == null)
            {
                return false;
            }

            try
            {
                _context.Doctors.Update(doctor);

                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        public void Delete(int id)
        {
            var toDelete = _context.Doctors.Find(id);

            if (toDelete != null)
            {
                _context.Doctors.Remove(toDelete);
                _context.SaveChanges();
            }
        }
    }
}
