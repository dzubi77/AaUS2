using AaUS2.structures;

namespace AaUS2.data_classes
{
    public class Person : IComparable<Person>
    {
        public required string PersonId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        //public AVLTree<PcrTest> Tests { get; set; } = new ();
        
        public int CompareTo(Person? other)
        { 
            throw new NotImplementedException();
        }

        public string ToString()
        {
            return "";
        }
    }
}
