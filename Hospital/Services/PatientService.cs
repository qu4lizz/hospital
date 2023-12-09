using Hospital.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class PatientService
    {
        private hospitalContext _context;

        public PatientService()
        {
            _context = new();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public void Add(Patient newPatient)
        {
            _context.Patients.Add(newPatient);
            _context.SaveChanges();
        }

        public void Update(Patient updatedPatient)
        {
            var existingPatient = _context.Patients.Find(updatedPatient.Id);

            if (existingPatient != null)
            {
                existingPatient.Name = updatedPatient.Name;
                existingPatient.Surname = updatedPatient.Surname;
                existingPatient.Address = updatedPatient.Address;
                existingPatient.Contact = updatedPatient.Contact;
                existingPatient.BirthDate = updatedPatient.BirthDate;

                _context.SaveChanges();
            }
        }

        public void Delete(int patientId)
        {
            var toDelete = _context.Patients.Find(patientId);

            if (toDelete != null)
            {
                _context.Patients.Remove(toDelete);
                _context.SaveChanges();
            }
        }
    }
}
