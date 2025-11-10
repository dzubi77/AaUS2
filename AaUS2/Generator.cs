using AaUS2.data_classes;
using System.Collections.ObjectModel;

namespace AaUS2
{
    public class Generator(int seed)
    {
        public Random Rng { get; } = new(seed);
        public ObservableCollection<string> Ids { get; } = new();

        public ObservableCollection<int> Regions { get; } = new();
        public ObservableCollection<int> Districts { get; } = new();
        public ObservableCollection<int> Places { get; } = new();

        // Udaje pre tieto dva atributy boli vygenerovane umelou inteligenciou.
        private readonly string[] FirstNames =
        {
            "John", "Michael", "Sarah", "Emma", "David", "Peter", "Daniel", "Sophia", "James", "Olivia",
            "Robert", "Ava", "William", "George", "Liam", "Mia", "Noah", "Charlotte", "Lucas", "Amelia"
        };

        private readonly string[] LastNames =
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin"
        };

        public Person GeneratePerson()
        {
            int year = Rng.Next(50, 100);
            int month = Rng.Next(1, 13);
            if (Rng.NextDouble() < 0.5) month += 50; 
            int day = Rng.Next(1, DateTime.DaysInMonth(year + 1900, month > 12 ? month - 50 : month) + 1);
            int code = Rng.Next(9999);

            string id = $"{year:00}{month:00}{day:00}{code}";
            Ids.Add(id);
            string firstName = FirstNames[Rng.Next(FirstNames.Length)];
            string lastName = LastNames[Rng.Next(LastNames.Length)];

            return new Person(id, firstName, lastName);
        }

        public IEnumerable<Person> GeneratePeople(int count)
        {
            for (int i = 0; i < count; i++)
                yield return GeneratePerson();
        }

        public PcrTest GeneratePcrTest()
        {
            if (!Districts.Any() || !Places.Any() || !Ids.Any())
                throw new InvalidOperationException("Generátor potrebuje aspoň jeden okres, miesto a osobu.");

            int testId = Rng.Next(1000, 9999);

            int districtIndex = Rng.Next(Districts.Count);
            int districtId = Districts[districtIndex];

            int districtsPerRegion = Districts.Count / Regions.Count;
            int regionId = ((districtId - 1) / districtsPerRegion) + 1;

            int placesPerDistrict = Places.Count / Districts.Count;
            int placeStartIndex = districtIndex * placesPerDistrict;
            int placeEndIndex = Math.Min(placeStartIndex + placesPerDistrict, Places.Count); 
            int placeId = Places[Rng.Next(placeStartIndex, placeEndIndex)];

            string patientId = Ids[Rng.Next(Ids.Count)];

            bool isPositive = Rng.NextDouble() < 0.2;
            double resultValue = Math.Round(Rng.NextDouble() * 10, 2);
            string note = "Generovaný test";
            DateTime testDateTime = DateTime.Now.AddDays(-Rng.Next(0, 30))
                .AddHours(Rng.Next(0, 24))
                .AddMinutes(Rng.Next(0, 60));

            return new PcrTest(testId, placeId, regionId, districtId, isPositive, resultValue, patientId, note, testDateTime);
        }

        public IEnumerable<PcrTest> GeneratePcrs(int count)
        {
            for (int i = 0; i < count; i++)
                yield return GeneratePcrTest();
        } 

        public void GenerateRegions(int count)
        {
            int start = Regions.Count > 0 ? Regions[^1] + 1 : 1;
            for (int i = 0; i < count; i++)
            {
                Regions.Add(start + i);
            }
        }

        public void GenerateDistricts(int count)
        {
            int start = Districts.Count > 0 ? Districts[^1] + 1 : 1;
            for (int i = 0; i < count; i++)
            {
                Districts.Add(start + i);
            }
        }

        public void GeneratePlaces(int count)
        {
            int start = Places.Count > 0 ? Places[^1] + 1 : 1;
            for (int i = 0; i < count; i++)
            {
                Places.Add(start + i);
            }
        }
    }
}
