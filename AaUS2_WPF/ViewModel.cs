using AaUS2;
using AaUS2.data_classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Input;

namespace AaUS2_WPF
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object>? canExecute;

        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);
        public void Execute(object parameter) => execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private readonly CoreApp core = new();
        private readonly Generator generator = new(100);

        // Collections
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<object> Results { get; set; }
        public ObservableCollection<int> RegionIds { get; set; } = new();
        public ObservableCollection<int> DistrictIds { get; set; } = new();
        public ObservableCollection<int> PlaceIds { get; set; } = new();
        public ObservableCollection<string> PeopleIds => generator.Ids; 

        // Generator inputs
        public int GeneratePeopleCount
        {
            get => _generatePeopleCount;
            set
            {
                if (value != _generatePeopleCount)
                {
                    _generatePeopleCount = value;
                    OnPropertyChanged(nameof(GeneratePeopleCount));
                }
            }
        }
        private int _generatePeopleCount;

        public int GenerateTestCount
        {
            get => _generateTestCount;
            set
            {
                if (value != _generateTestCount)
                {
                    _generateTestCount = value;
                    OnPropertyChanged(nameof(GenerateTestCount));
                }
            }
        }
        private int _generateTestCount;

        public int GenerateRegionCount { get; set; }
        public int GenerateDistrictCount { get; set; }
        public int GeneratePlaceCount { get; set; }

        // Selected for Test insertion
        public int SelectedRegionId { get; set; }
        public int SelectedDistrictId { get; set; }
        public int SelectedPlaceId { get; set; }

        // Person form inputs
        public string NewPersonId { get => _newPersonId; set { _newPersonId = value; OnPropertyChanged(nameof(NewPersonId)); } }
        private string _newPersonId;
        public string NewPersonFirstName { get => _newPersonFirstName; set { _newPersonFirstName = value; OnPropertyChanged(nameof(NewPersonFirstName)); } }
        private string _newPersonFirstName;
        public string NewPersonLastName { get => _newPersonLastName; set { _newPersonLastName = value; OnPropertyChanged(nameof(NewPersonLastName)); } }
        private string _newPersonLastName;

        // Test form inputs
        private int _newTestId = 1000;
        public string NewTestPatientId { get => _newTestPatientId; set { _newTestPatientId = value; OnPropertyChanged(nameof(NewTestPatientId)); } }
        private string _newTestPatientId;
        public double NewTestResultValue { get => _newTestResultValue; set { _newTestResultValue = value; OnPropertyChanged(nameof(NewTestResultValue)); } }
        private double _newTestResultValue;
        public bool NewTestIsPositive { get => _newTestIsPositive; set { _newTestIsPositive = value; OnPropertyChanged(nameof(NewTestIsPositive)); } }
        private bool _newTestIsPositive;
        public DateTime? NewTestDate { get => _newTestDate; set { _newTestDate = value; OnPropertyChanged(nameof(NewTestDate)); } }
        private DateTime? _newTestDate;
        public string NewTestTime { get => _newTestTime; set { _newTestTime = value; OnPropertyChanged(nameof(NewTestTime)); } }
        private string _newTestTime;
        public string NewTestNote { get => _newTestNote; set { _newTestNote = value; OnPropertyChanged(nameof(NewTestNote)); } }
        private string _newTestNote;

        // Search inputs
        public int SearchTestId { get => _searchTestId; set { _searchTestId = value; OnPropertyChanged(nameof(SearchTestId)); } }
        private int _searchTestId;
        public string SearchPersonId { get => _searchPersonId; set { _searchPersonId = value; OnPropertyChanged(nameof(SearchPersonId)); } }
        private string _searchPersonId;

        // Territorial units and date search inputs
        public int ReportDistrictId { get => _reportDistrictId; set { _reportDistrictId = value; OnPropertyChanged(nameof(ReportDistrictId)); } }
        private int _reportDistrictId;
        public int ReportRegionId { get => _reportRegionId; set { _reportRegionId = value; OnPropertyChanged(nameof(ReportRegionId)); } }
        private int _reportRegionId;
        public int ReportPlaceId { get => _reportPlaceId; set { _reportPlaceId = value; OnPropertyChanged(nameof(ReportPlaceId)); } }
        private int _reportPlaceId;
        public DateTime? ReportFromDate { get => _reportFromDate; set { _reportFromDate = value; OnPropertyChanged(nameof(ReportFromDate)); } }
        private DateTime? _reportFromDate;
        public DateTime? ReportToDate { get => _reportToDate; set { _reportToDate = value; OnPropertyChanged(nameof(ReportToDate)); } }
        private DateTime? _reportToDate;

        // Sick search
        public int SickDistrictId { get => _sickDistrictId; set { _sickDistrictId = value; OnPropertyChanged(nameof(SickDistrictId)); } }
        private int _sickDistrictId;
        public int SickRegionId { get => _sickRegionId; set { _sickRegionId = value; OnPropertyChanged(nameof(SickRegionId)); } }
        private int _sickRegionId;
        public DateTime? SickDate { get => _sickDate; set { _sickDate = value; OnPropertyChanged(nameof(SickDate)); } }
        private DateTime? _sickDate;
        public int SickDays { get => _sickDays; set { _sickDays = value; OnPropertyChanged(nameof(SickDays)); } }
        private int _sickDays;

        // Commands
        public ICommand InsertPersonCommand { get; }
        public ICommand DeletePersonCommand { get; }
        public ICommand InsertTestCommand { get; }
        public ICommand DeleteTestCommand { get; }
        public ICommand GeneratePeopleCommand { get; }
        public ICommand GenerateTestsCommand { get; }
        public ICommand GenerateRegionsDistrictsPlacesCommand { get; }
        public ICommand ClearRegionsDistrictsPlacesCommand { get; }

        // Search & Reports
        public ICommand FindTestByIdCommand { get; }
        public ICommand FindTestsForPatientCommand { get; }
        public ICommand FindTestsByDistrictCommand { get; }
        public ICommand FindPositiveByDistrictCommand { get; }
        public ICommand FindTestsByRegionCommand { get; }
        public ICommand FindPositiveByRegionCommand { get; }
        public ICommand FindTestsByPlaceCommand { get; }
        public ICommand FindTestsByPeriodCommand { get; }
        public ICommand FindPositiveByPeriodCommand { get; }
        public ICommand FindSickByDistrictCommand { get; }
        public ICommand FindSickByDistrictOrderedCommand { get; }
        public ICommand FindSickByRegionCommand { get; }
        public ICommand FindSickAllCommand { get; }
        public ICommand FindTopSickByDistrictCommand { get; }
        public ICommand FindDistrictsBySickCountCommand { get; }
        public ICommand FindRegionsBySickCountCommand { get; }

        public ViewModel()
        {
            People = new ObservableCollection<Person>();
            Results = new ObservableCollection<object>();

            InsertPersonCommand = new RelayCommand(_ => InsertPerson());
            DeletePersonCommand = new RelayCommand(id => DeletePerson(id?.ToString()));
            InsertTestCommand = new RelayCommand(_ => InsertTest());
            DeleteTestCommand = new RelayCommand(id => DeleteTest(Convert.ToInt32(id)));

            GeneratePeopleCommand = new RelayCommand(_ => GeneratePeople());
            GenerateTestsCommand = new RelayCommand(_ => GenerateTests());
            GenerateRegionsDistrictsPlacesCommand = new RelayCommand(_ => GenerateRegionsDistrictsPlaces());
            ClearRegionsDistrictsPlacesCommand = new RelayCommand(_ => ClearRegionsDistrictsPlaces());

            FindTestByIdCommand = new RelayCommand(_ => FindTestById());
            FindTestsForPatientCommand = new RelayCommand(_ => FindTestsForPatient());

            FindTestsByDistrictCommand = new RelayCommand(_ => FindTestsByDistrict());
            FindPositiveByDistrictCommand = new RelayCommand(_ => FindPositiveByDistrict());
            FindTestsByRegionCommand = new RelayCommand(_ => FindTestsByRegion());
            FindPositiveByRegionCommand = new RelayCommand(_ => FindPositiveByRegion());
            FindTestsByPlaceCommand = new RelayCommand(_ => FindTestsByPlace());
            FindTestsByPeriodCommand = new RelayCommand(_ => FindTestsByPeriod());
            FindPositiveByPeriodCommand = new RelayCommand(_ => FindPositiveByPeriod());

            FindSickByDistrictCommand = new RelayCommand(_ => FindSickByDistrict());
            FindSickByDistrictOrderedCommand = new RelayCommand(_ => FindSickByDistrictOrdered());
            FindSickByRegionCommand = new RelayCommand(_ => FindSickByRegion());
            FindSickAllCommand = new RelayCommand(_ => FindSickAll());
            FindTopSickByDistrictCommand = new RelayCommand(_ => FindTopSickByDistrict());
            FindDistrictsBySickCountCommand = new RelayCommand(_ => FindDistrictsBySickCount());
            FindRegionsBySickCountCommand = new RelayCommand(_ => FindRegionsBySickCount());
        }

        private void InsertPerson()
        {
            var person = new Person(NewPersonId, NewPersonFirstName, NewPersonLastName);
            core.InsertPerson(person);
            People.Add(person);
            PeopleIds.Add(NewPersonId);
        } 

        private void DeletePerson(string id)
        {
            if (string.IsNullOrEmpty(id)) return;
            PeopleIds.Remove(id);
            core.RemovePerson(id);
            var person = People.FirstOrDefault(p => p.PersonId == id);
            if (person != null) People.Remove(person);
        }

        private void InsertTest()
        {
            var date = NewTestDate ?? DateTime.Now;
            var time = TimeOnly.TryParse(NewTestTime, out var parsedTime) ? parsedTime : TimeOnly.MinValue;
            var dateTime = date.Date + time.ToTimeSpan();

            var test = new PcrTest(_newTestId++, SelectedPlaceId, SelectedRegionId, SelectedDistrictId,
                NewTestIsPositive, NewTestResultValue, NewTestPatientId, NewTestNote, dateTime);

            core.InsertPcr(test);
            Results.Add(test);
        }

        private void DeleteTest(int testId)
        {
            core.RemovePcr(testId);
        }

        private void GeneratePeople()
        {
            var newPeople = generator.GeneratePeople(GeneratePeopleCount);
            foreach (var p in newPeople)
            {
                core.InsertPerson(p);
                People.Add(p);
            }
        }

        private void GenerateTests()
        {
            if (!People.Any() || !generator.Regions.Any() || !generator.Districts.Any() || !generator.Places.Any())
                return;

            var newTests = generator.GeneratePcrs(GenerateTestCount);
            foreach (var t in newTests)
            {
                core.InsertPcr(t);
                Results.Add(t);
            }
        }

        private void GenerateRegionsDistrictsPlaces()
        {
            generator.GenerateRegions(GenerateRegionCount);
            generator.GenerateDistricts(GenerateDistrictCount);
            generator.GeneratePlaces(GeneratePlaceCount);

            RegionIds.Clear();
            foreach (var r in generator.Regions) RegionIds.Add(r);

            DistrictIds.Clear();
            foreach (var d in generator.Districts) DistrictIds.Add(d);

            PlaceIds.Clear();
            foreach (var p in generator.Places) PlaceIds.Add(p);
        }

        private void ClearRegionsDistrictsPlaces()
        {
            generator.Regions.Clear();
            generator.Districts.Clear();
            generator.Places.Clear();
            RegionIds.Clear();
            DistrictIds.Clear();
            PlaceIds.Clear();
        }

        private void FindTestById() => UpdateResults(new[] { core.FindPcrByCode(SearchTestId) });
        private void FindTestsForPatient() => UpdateResults(core.FindAllPatientTests(SearchPersonId));
        private void FindTestsByDistrict() => UpdateResults(core.FindAllTestsForDistrict(ReportDistrictId, ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindPositiveByDistrict() => UpdateResults(core.FindPositiveTestsForDistrict(ReportDistrictId, ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindTestsByRegion() => UpdateResults(core.FindAllTestsForRegion(ReportRegionId, ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindPositiveByRegion() => UpdateResults(core.FindPositiveTestsForRegion(ReportRegionId, ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindTestsByPlace() => UpdateResults(core.FindAllTestsByPlace(ReportPlaceId, ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindTestsByPeriod() => UpdateResults(core.FindAllTestsWithin(ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindPositiveByPeriod() => UpdateResults(core.FindPositiveTestsWithin(ToDateOnly(ReportFromDate), ToDateOnly(ReportToDate)));
        private void FindSickByDistrict() => UpdateResults(core.FindPatientsByDistrict(SickDays, SickDistrictId, ToDateOnly(SickDate)));
        private void FindSickByDistrictOrdered() => UpdateResults(core.FindAndSortPatientsByDistrict(SickDays, SickDistrictId, ToDateOnly(SickDate)));
        private void FindSickByRegion() => UpdateResults(core.FindPatientsByRegion(SickDays, SickRegionId, ToDateOnly(SickDate)));
        private void FindSickAll() => UpdateResults(core.FindAllPatientsWithin(SickDays, ToDateOnly(SickDate)));
        private void FindTopSickByDistrict() => UpdateResults(core.FindTopSickByDistrict(ToDateOnly(SickDate), SickDays));
        private void FindDistrictsBySickCount() => UpdateResults(core.FindDistrictsBySickCount(ToDateOnly(SickDate), SickDays));
        private void FindRegionsBySickCount() => UpdateResults(core.FindRegionsBySickCount(ToDateOnly(SickDate), SickDays));

        private void UpdateResults(System.Collections.IEnumerable collection)
        {
            Results.Clear();
            foreach (var item in collection)
                Results.Add(item);
        }

        private static DateOnly ToDateOnly(DateTime? date) => date.HasValue ? DateOnly.FromDateTime(date.Value) : DateOnly.MinValue;
    }
}