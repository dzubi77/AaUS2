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
        public ReadOnlyCollection<PcrByDate> FindAllPatientTests(string patientId)
        {
            var personDummy = new Person(patientId);
            var patient = Patients.Find(personDummy);
            var tree = patient.TestsByDate;
            List<PcrByDate> tests = new List<PcrByDate>(patient.Tests.Size);
            tree.ProcessInOrder(tree.Root, (n) => tests.Add(n.Data));
            return tests.AsReadOnly(); 
        } 
        
        // 4
        public ReadOnlyCollection<PcrByDistrict> FindPositiveTestsForDistrict(int districtId, DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        }
        
        // 5
        public ReadOnlyCollection<PcrByDistrict> FindAllTestsForDistrict(int districtId, DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        }
        
        // 6
        public ReadOnlyCollection<PcrByRegion> FindPositiveTestsForRegion(int regionId, DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        }
        
        // 7
        public ReadOnlyCollection<PcrByRegion> FindAllTestsForRegion(int regionId, DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        }

        // 8
        public ReadOnlyCollection<PcrByDate> FindPositiveTestsWithin(DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        }

        // 9
        public ReadOnlyCollection<PcrByDate> FindAllTestsWithin(DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        } 

        // 10
        public ReadOnlyCollection<Person> FindPatientsByDistrict(int x, int districtId, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 11
        public ReadOnlyCollection<Person> FindAndSortPatientsByDistrict(int x, int districtId, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 12
        public ReadOnlyCollection<Person> FindPatientsByRegion(int x, int regionId, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 13
        public ReadOnlyCollection<Person> FindAllPatientsWithin(int x, DateOnly currentDate)
        {
            throw new NotImplementedException();
        }

        // 14
        public Person FindMostIllPatient(int x, DateOnly currentDate)
        {
            throw new NotImplementedException(); 
        }

        // 15
        public void SortDistrictsByIll() {}

        // 16
        public void SortRegionsByIll() {}

        // 17
        public ReadOnlyCollection<PcrByPlace> FindAllTestsByPlace(int placeId, DateOnly dateFrom, DateOnly dateTo)
        {
            throw new NotImplementedException();
        }

        // 18
        public PcrTest FindPcrByCode(int testId)
        {
            throw new NotImplementedException();
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
            var testTreeByDate = patient.TestsByDate;
            testTree.ProcessInOrder(testTree.Root, (n) =>
                {
                    var t = n.Data.Test;
                    Tests.Remove(new PcrById(t));
                    TestsByDistrict.Remove(new PcrByDistrict(t));
                    TestsByRegion.Remove(new PcrByRegion(t));
                    TestsByPlace.Remove(new PcrByPlace(t));
                    TestsByDate.Remove(new PcrByDate(t));  
                });
            testTreeByDate.ProcessInOrder(testTreeByDate.Root, (n) =>
                {

                });
        }
    }
}
