using System;
using System.ComponentModel;

namespace Hospital.DataObjects
{
    public partial class Admission : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private DateTime _admissionDate;
        public DateTime AdmissionDate
        {
            get { return _admissionDate; }
            set
            {
                if (_admissionDate != value)
                {
                    _admissionDate = value;
                    OnPropertyChanged(nameof(AdmissionDate));
                }
            }
        }

        private DateTime _dischargeDate;
        public DateTime DischargeDate
        {
            get { return _dischargeDate; }
            set
            {
                if (_dischargeDate != value)
                {
                    _dischargeDate = value;
                    OnPropertyChanged(nameof(DischargeDate));
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
