using System.Collections.ObjectModel;
using AaUS2.data_classes;
using AaUS2.data_classes.wrappers;
using AaUS2.structures;

namespace AaUS2
{
    /**
     * Represents object that manipulates with data and data structures.
     * GUI uses its operations.
     */
    public class CoreApp
    {
        public AVLTree<Person> Patients { get; } = new();
        public AVLTree<PcrById> Tests { get; } = new();
        public AVLTree<PcrByDistrict> TestsByDistrict { get; } = new();
        public AVLTree<PcrByRegion> TestsByRegion { get; } = new();
        public AVLTree<PcrByPlace> TestsByPlace { get; } = new();
        public AVLTree<PcrByDate> TestsByDate { get; } = new();

        // 1
        public void InsertPcr(PcrTest test)
        {
            PcrById pcr = new PcrById(test);
            PcrByDate pcrByDate = new PcrByDate(test);

            var personDummy = new Person(test.PatientId);
            var patient = Patients.Find(personDummy);
            patient.Tests.Insert(pcr);
            patient.TestsByDate.Insert(pcrByDate); 

            Tests.Insert(pcr);
            TestsByDate.Insert(pcrByDate);

            TestsByDistrict.Insert(new PcrByDistrict(test));
            TestsByRegion.Insert(new PcrByRegion(test));
            TestsByPlace.Insert(new PcrByPlace(test));
        }

        // 2
        public PcrTest FindPcrResultForPatient(int testId, string patientId)
        {
            var personDummy = new Person(patientId);
            var patient = Patients.Find(personDummy);
            var pcrDummy = new PcrById(new PcrTest(testId));
            var result = patient.Tests.Find(pcrDummy);
            return result.Test; 
        }

        // 3
        public ObservableCollection<PcrByDate> FindAllPatientTests(string patientId)
        {
            var personDummy = new Person(patientId);
            var patient = Patients.Find(personDummy);
            var tree = patient.TestsByDate;
            List<PcrByDate> tests = new List<PcrByDate>(patient.Tests.Size);
            tree.ProcessInOrder(tree.Root, (n) => tests.Add(n.Data));
            throw new NotImplementedException();
        }

        // 4
        public ObservableCollection<PcrByDistrict> FindPositiveTestsForDistrict(int districtId, DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 5
        public ObservableCollection<PcrByDistrict> FindAllTestsForDistrict(int districtId, DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 6
        public ObservableCollection<PcrByRegion> FindPositiveTestsForRegion(int regionId, DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 7
        public ObservableCollection<PcrByRegion> FindAllTestsForRegion(int regionId, DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 8
        public ObservableCollection<PcrByDate> FindPositiveTestsWithin(DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 9
        public ObservableCollection<PcrByDate> FindAllTestsWithin(DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 10
        public ObservableCollection<Person> FindPatientsByDistrict(int x, int districtId, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 11
        public ObservableCollection<Person> FindAndSortPatientsByDistrict(int x, int districtId, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 12
        public ObservableCollection<Person> FindPatientsByRegion(int x, int regionId, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 13
        public ObservableCollection<Person> FindAllPatientsWithin(int x, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 14 
        public ObservableCollection<PcrByDistrict> FindTopSickByDistrict(DateOnly? date, int x)
        {
            throw new NotImplementedException(); 
        }

        // 15
        public ObservableCollection<PcrByDistrict> FindDistrictsBySickCount(DateOnly? date, int xDays)
        {
            throw new NotImplementedException();
        }

        // 16
        public ObservableCollection<PcrByRegion> FindRegionsBySickCount(DateOnly? date, int xDays)
        {
            throw new NotImplementedException();
        }

        // 17
        public ObservableCollection<PcrByPlace> FindAllTestsByPlace(int placeId, DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        // 18
        public PcrTest FindPcrByCode(int testId)
        {
            var testDummy = new PcrById(new PcrTest(testId)); 
            return Tests.Find(testDummy).Test;
        }

        // 19
        public void InsertPerson(Person person)
        {
            Patients.Insert(person); 
        }

        // 20
        public void RemovePcr(int testId)
        {
            var testDummy = new PcrTest(testId);
            var testToRemove = Tests.Find(new PcrById(testDummy));
            var testByDateToRemove = new PcrByDate(testDummy);
            Tests.Remove(testToRemove);
            TestsByDistrict.Remove(new PcrByDistrict(testDummy));
            TestsByRegion.Remove(new PcrByRegion(testDummy));
            TestsByPlace.Remove(new PcrByPlace(testDummy));
            TestsByDate.Remove(testByDateToRemove);

            var patient = Patients.Find(new Person(testToRemove.Test.PatientId));
            patient.Tests.Remove(testToRemove);
            patient.TestsByDate.Remove(testByDateToRemove);
        }

        // 21
        public void RemovePerson(string patientId)
        {
            var patient = Patients.Find(new Person(patientId));
            var testTree = patient.Tests;
            testTree.ProcessInOrder(testTree.Root, (n) =>
                {
                    var t = n.Data.Test;
                    Tests.Remove(new PcrById(t));
                    TestsByDistrict.Remove(new PcrByDistrict(t));
                    TestsByRegion.Remove(new PcrByRegion(t));
                    TestsByPlace.Remove(new PcrByPlace(t));
                    TestsByDate.Remove(new PcrByDate(t));  
                });
        }
    }
}
