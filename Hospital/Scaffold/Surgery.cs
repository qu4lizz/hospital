using System;
using System.Collections.Generic;

namespace Hospital.Scaffold
{
    public partial class Surgery
    {
        public int IdSurgery { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = null!;
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
