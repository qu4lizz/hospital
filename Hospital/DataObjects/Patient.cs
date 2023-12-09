using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.DataObjects
{
    public partial class Patient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Patient()
        {
            Admissions = new HashSet<Admission>();
            Appointments = new HashSet<Appointment>();
            Records = new HashSet<Record>();
            Surgeries = new HashSet<Surgery>();
        }

        public int Id { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _surname = string.Empty;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }

        private string _contact = string.Empty;
        public string Contact
        {
            get { return _contact; }
            set
            {
                if (_contact != value)
                {
                    _contact = value;
                    OnPropertyChanged(nameof(Contact));
                }
            }
        }

        private string _address = string.Empty;
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged(nameof(BirthDate));
                }
            }
        }

        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Surgery> Surgeries { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{Name} {Surname} ({Contact})";
        }
    }
}
