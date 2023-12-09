using Hospital.DataObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class AppointmentsService
    {
        private readonly hospitalContext _context;

        public AppointmentsService()
        {
            _context = new hospitalContext();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderBy(a => a.Date)
                .ToList();
        }

        public IEnumerable<Appointment> GetAllByDoctor(int doctorId)
        {
            return _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient)
                .OrderBy(a => a.Date)
                .ToList();
        }

        public void Add(Appointment newAppointment)
        {
            _context.Appointments.Add(newAppointment);
            _context.SaveChanges();
        }

        public void Update(Appointment updatedAppointment)
        {
            var existingAppointment = _context.Appointments.Find(updatedAppointment.Id);

            if (existingAppointment != null)
            {
                existingAppointment.Date = updatedAppointment.Date;
                existingAppointment.DoctorId = updatedAppointment.DoctorId;
                existingAppointment.PatientId = updatedAppointment.PatientId;

                _context.SaveChanges();
            }
        }

        public void Delete(int appointmentId)
        {
            var appointmentToDelete = _context.Appointments.Find(appointmentId);

            if (appointmentToDelete != null)
            {
                _context.Appointments.Remove(appointmentToDelete);
                _context.SaveChanges();
            }
        }
    }
}
