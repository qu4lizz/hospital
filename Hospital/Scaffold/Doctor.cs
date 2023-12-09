using System;
using System.Collections.Generic;

namespace Hospital.Scaffold
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            Records = new HashSet<Record>();
            Surgeries = new HashSet<Surgery>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Theme { get; set; } = null!;
        public string Language { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Surgery> Surgeries { get; set; }
    }
}
