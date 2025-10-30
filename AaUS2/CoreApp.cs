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
            Tests.Insert(pcr);
            TestsByDistrict.Insert(new PcrByDistrict(test));
            TestsByRegion.Insert(new PcrByRegion(test));

            var personDummy = new Person(test.PatientId);
            var patient = Patients.Find(personDummy);
            patient.Tests.Insert(pcr); 
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
        public List<PcrById> FindAllPatientTests(string patientId)
        {
            var personDummy = new Person(patientId);
            var patient = Patients.Find(personDummy);
            List<PcrById> tests = new List<PcrById>(patient.Tests.Size);
            patient.Tests.ProcessInOrder(patient.Tests.Root, (n) => tests.Add(n.Data));
            // sort tests, or add tree to patient (sorted by date)
            return tests;
        }
        
        // 4
        public void FindPositiveTestsForDistrict() {}
        
        // 5
        public void FindAllTestsForDistrict() {}
        
        // 6
        public void FindPositiveTestsForRegion() {}
        
        // 7
        public void FindAllTestsForRegion() {}

        // 8
        public void FindPositiveTestsWithin() {}

        // 9
        public void FindAllTestsWithin() {}

        // 10
        public void FindPatientsByDistrict() {}

        // 11
        public void FindAndSortPatientsByDistrict() {}

        // 12
        public void FindPatientsByRegion() {}

        // 13
        public void FindAllPatientsWithin() {}

        // 14
        public void FindMostIllPatient() {}

        // 15
        public void SortDistrictsByIll() {}

        // 16
        public void SortRegionsByIll() {}

        // 17
        public void FindAllTestsByPlace() {}

        // 18
        public void FindPcrByCode() {}

        // 19
        public void InsertPerson() {}

        // 20
        public void RemovePcr(int testId)
        {
            var tmp = new PcrTest(testId);
            Tests.Remove(new PcrById(tmp));
            TestsByDistrict.Remove(new PcrByDistrict(tmp));
            TestsByRegion.Remove(new PcrByRegion(tmp));
        }

        // 21
        public void RemovePerson() {}
    }
}
