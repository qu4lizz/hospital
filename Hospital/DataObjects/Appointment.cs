using System;
using System.ComponentModel;

namespace Hospital.DataObjects
{
    public partial class Appointment : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private int _doctorId;
        public int DoctorId
        {
            get { return _doctorId; }
            set
            {
                if (_doctorId != value)
                {
                    _doctorId = value;
                    OnPropertyChanged(nameof(DoctorId));
                }
            }
        }

        private int _patientId;
        public int PatientId
        {
            get { return _patientId; }
            set
            {
                if (_patientId != value)
                {
                    _patientId = value;
                    OnPropertyChanged(nameof(PatientId));
                }
            }
        }

        private Doctor _doctor = null!;
        public virtual Doctor Doctor
        {
            get { return _doctor; }
            set
            {
                if (_doctor != value)
                {
                    _doctor = value;
                    OnPropertyChanged(nameof(Doctor));
                }
            }
        }

        private Patient _patient = null!;
        public virtual Patient Patient
        {
            get { return _patient; }
            set
            {
                if (_patient != value)
                {
                    _patient = value;
                    OnPropertyChanged(nameof(Patient));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
