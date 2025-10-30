using AaUS2.data_classes.wrappers;
using AaUS2.structures;

namespace AaUS2.data_classes
{
    /**
     * Objects representing patients. PersonId must be unique.
     */
    public class Person(string personId, string firstName, string lastName) : IComparable<Person>
    {
        public string PersonId { get; } = personId;
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public DateOnly BirthDate { get; } = GetBirthDate(personId);
        public AVLTree<PcrById> Tests { get; } = new(); 
        public AVLTree<PcrByDate> TestsByDate { get; } = new();
        
        public Person(string personId) : this(personId, "", "") {}

        public int CompareTo(Person? other)
        { 
            throw new NotImplementedException();
        }

        /**
         * Returns string that could be used to save in csv.
         */
        public override string ToString()
        {
            return PersonId + ";" + FirstName + ";" + LastName + ";" + HlpClass.DateToString(BirthDate);  
        }

        /**
         * Helper operation to get birthdate based on person id.
         */
        private static DateOnly GetBirthDate(string personId)
        {
            if (personId.Length < 6) throw new ArgumentException("Invalid id format!");
            int year = int.Parse(personId.Substring(0, 2));
            if (year < 25)
            {
                year = 2000 + year;
            }
            else
            {
                year = 1900 + year;
            }
            int month = int.Parse(personId.Substring(2, 2));
            month %= 50;
            int day = int.Parse(personId.Substring(4, 2));
            return new DateOnly(year, month, day); 
        }
    }
}
