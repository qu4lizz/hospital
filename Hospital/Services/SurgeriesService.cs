using Hospital.DataObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class SurgeriesService
    {
        private readonly hospitalContext _context;

        public SurgeriesService()
        {
            _context = new hospitalContext();
        }

        public IEnumerable<Surgery> GetAll()
        {
            return _context.Surgeries
                .Include(s => s.Doctor)
                .Include(s => s.Patient)
                .OrderBy(s => s.Date)
                .ToList();
        }

        public IEnumerable<Surgery> GetAllByDoctor(int doctorId)
        {
            return _context.Surgeries
                .Where(s => s.DoctorId == doctorId)
                .Include(s => s.Patient)
                .OrderBy(s => s.Date)
                .ToList();
        }

        public void Add(Surgery newSurgery)
        {
            _context.Surgeries.Add(newSurgery);
            _context.SaveChanges();
        }

        public void Update(Surgery updatedSurgery)
        {
            var existingSurgery = _context.Surgeries.Find(updatedSurgery.IdSurgery);

            if (existingSurgery != null)
            {
                existingSurgery.Date = updatedSurgery.Date;
                existingSurgery.DoctorId = updatedSurgery.DoctorId;
                existingSurgery.PatientId = updatedSurgery.PatientId;
                existingSurgery.Notes = updatedSurgery.Notes;

                _context.SaveChanges();
            }
        }

        public void Delete(int surgeryId)
        {
            var surgeryToDelete = _context.Surgeries.Find(surgeryId);

            if (surgeryToDelete != null)
            {
                _context.Surgeries.Remove(surgeryToDelete);
                _context.SaveChanges();
            }
        }
    }
}
