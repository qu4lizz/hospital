using System;
using System.ComponentModel;

namespace Hospital.DataObjects
{
    public partial class Record : INotifyPropertyChanged
    {
        public int Id { get; set; }

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

        private string _diagnosis = string.Empty;
        public string Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    OnPropertyChanged(nameof(Diagnosis));
                }
            }
        }

        private string _prescription = string.Empty;
        public string Prescription
        {
            get { return _prescription; }
            set
            {
                if (_prescription != value)
                {
                    _prescription = value;
                    OnPropertyChanged(nameof(Prescription));
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
