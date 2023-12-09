using Hospital.DataObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class RecordsService
    {
        private readonly hospitalContext _context;

        public RecordsService()
        {
            _context = new hospitalContext();
        }

        public IEnumerable<Record> GetAll()
        {
            return _context.Records
                .Include(r => r.Doctor)
                .Include(r => r.Patient)
                .OrderBy(r => r.Date)
                .ToList();
        }

        public IEnumerable<Record> GetAllByDoctor(int doctorId)
        {
            return _context.Records
                .Where(r => r.DoctorId == doctorId)
                .Include(r => r.Patient)
                .OrderBy(r => r.Date)
                .ToList();
        }

        public void Add(Record newRecord)
        {
            _context.Records.Add(newRecord);
            _context.SaveChanges();
        }

        public void Update(Record updatedRecord)
        {
            var existingRecord = _context.Records.Find(updatedRecord.Id);

            if (existingRecord != null)
            {
                existingRecord.Date = updatedRecord.Date;
                existingRecord.DoctorId = updatedRecord.DoctorId;
                existingRecord.PatientId = updatedRecord.PatientId;
                existingRecord.Diagnosis = updatedRecord.Diagnosis;
                existingRecord.Prescription = updatedRecord.Prescription;

                _context.SaveChanges();
            }
        }

        public void Delete(int recordId)
        {
            var recordToDelete = _context.Records.Find(recordId);

            if (recordToDelete != null)
            {
                _context.Records.Remove(recordToDelete);
                _context.SaveChanges();
            }
        }
    }
}
