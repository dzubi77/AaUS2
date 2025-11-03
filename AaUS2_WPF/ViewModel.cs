using AaUS2;
using AaUS2.data_classes;
using AaUS2.data_classes.wrappers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AaUS2_WPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // backend
        private readonly CoreApp _coreApp = new();

        // collections
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<object> Results { get; set; }

        // Person form inputs
        public string NewPersonId
        { 
            get => _newPersonId;
            set
            {
                _newPersonId = value; 
                OnPropertyChanged(nameof(NewPersonId));
            }
        }
        private string _newPersonId;

        public string NewPersonFirstName
        {
            get => _newPersonFirstName; 
            set 
            {
                _newPersonFirstName = value; 
                OnPropertyChanged(nameof(NewPersonFirstName));
            }
        }
        private string _newPersonFirstName;

        public string NewPersonLastName 
        { 
            get => _newPersonLastName;
            set
            {
                _newPersonLastName = value; 
                OnPropertyChanged(nameof(NewPersonLastName));
            }
        }
        private string _newPersonLastName;

        public DateTime? NewPersonBirthDate
        {
            get => _newPersonBirthDate;
            set
            {
                _newPersonBirthDate = value; 
                OnPropertyChanged(nameof(NewPersonBirthDate));
            }
        }
        private DateTime? _newPersonBirthDate;

        // Test form inputs
        public int NewTestId
        {
            get => _newTestId;
            set
            {
                _newTestId = value; 
                OnPropertyChanged(nameof(NewTestId));
            }
        }
        private int _newTestId;

        public string NewTestPatientId
        {
            get => _newTestPatientId;
            set
            {
                _newTestPatientId = value; 
                OnPropertyChanged(nameof(NewTestPatientId));
            }
        }
        private string _newTestPatientId;

        public int NewTestDistrictId
        {
            get => _newTestDistrictId;
            set
            {
                _newTestDistrictId = value; 
                OnPropertyChanged(nameof(NewTestDistrictId));
            }
        }
        private int _newTestDistrictId;

        public int NewTestRegionId
        {
            get => _newTestRegionId;
            set
            {
                _newTestRegionId = value; 
                OnPropertyChanged(nameof(NewTestRegionId));
            }
        }
        private int _newTestRegionId;

        public int NewTestPlaceId
        {
            get => _newTestPlaceId;
            set
            {
                _newTestPlaceId = value; 
                OnPropertyChanged(nameof(NewTestPlaceId));
            }
        }
        private int _newTestPlaceId;

        public string NewTestResultValue
        {
            get => _newTestResultValue;
            set
            {
                _newTestResultValue = value; 
                OnPropertyChanged(nameof(NewTestResultValue));
            }
        }
        private string _newTestResultValue;

        public bool NewTestIsPositive
        {
            get => _newTestIsPositive;
            set
            {
                _newTestIsPositive = value; 
                OnPropertyChanged(nameof(NewTestIsPositive));
            }
        }
        private bool _newTestIsPositive;

        public DateTime? NewTestDate
        {
            get => _newTestDate; 
            set 
            { 
                _newTestDate = value; 
                OnPropertyChanged(nameof(NewTestDate));
            }
        }
        private DateTime? _newTestDate;

        public string NewTestTime
        {
            get => _newTestTime;
            set
            {
                _newTestTime = value; 
                OnPropertyChanged(nameof(NewTestTime));
            }
        }
        private string _newTestTime;

        public string NewTestNote
        {
            get => _newTestNote;
            set
            {
                _newTestNote = value; 
                OnPropertyChanged(nameof(NewTestNote));
            }
        }
        private string _newTestNote;

        // Commands


        public ViewModel()
        {
            // Inicializácia kolekcií
            People = new ObservableCollection<Person>();
            Results = new ObservableCollection<object>();

            // Inicializácia príkazov
            /*
            InsertPersonCommand = new RelayCommand(_ => InsertPerson());
            DeletePersonCommand = new RelayCommand(id => DeletePerson(id?.ToString()));

            InsertTestCommand = new RelayCommand(_ => InsertTest());
            DeleteTestCommand = new RelayCommand(id => DeleteTest(Convert.ToInt32(id)));

            FindTestByIdCommand = new RelayCommand(_ => FindTestById());
            FindTestsForPatientCommand = new RelayCommand(_ => FindTestsForPatient());
            */
        }
    }
}
