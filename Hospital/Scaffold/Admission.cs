using System;
using System.Collections.Generic;

namespace Hospital.Scaffold
{
    public partial class Admission
    {
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; } = null!;
    }
}
