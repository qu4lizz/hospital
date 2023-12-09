using System;
using System.Collections.Generic;

namespace Hospital.Scaffold
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorIdDoctor { get; set; }
        public int PatientIdPatient { get; set; }

        public virtual Doctor DoctorIdDoctorNavigation { get; set; } = null!;
        public virtual Patient PatientIdPatientNavigation { get; set; } = null!;
    }
}
