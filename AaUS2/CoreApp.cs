using AaUS2.data_classes;
using AaUS2.structures;

namespace AaUS2
{
    public class CoreApp
    {
        public AVLTree<Person> Patients { get; set; } = new();
        // TODO: add necessary AVLs

        // 1
        public void InsertPcr() {} 
        
        // 2
        public void FindPcrResultForPatient() {}
        
        // 3
        public void FindAllPatientTests() {}
        
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
        public void RemovePcr() {}

        // 21
        public void RemovePerson() {}
    }
}
