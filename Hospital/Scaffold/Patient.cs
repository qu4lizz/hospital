using System;
using System.Collections.Generic;

namespace Hospital.Scaffold
{
    public partial class Patient
    {
        public Patient()
        {
            Admissions = new HashSet<Admission>();
            Appointments = new HashSet<Appointment>();
            Records = new HashSet<Record>();
            Surgeries = new HashSet<Surgery>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Surgery> Surgeries { get; set; }
    }
}
