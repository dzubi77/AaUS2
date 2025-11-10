namespace AaUS2.data_classes.wrappers
{
    public class PcrById(PcrTest test) : IComparable<PcrById>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrById? other)
        {
            if (other == null) return 1; 
            return Test.TestId.CompareTo(other.Test.TestId);
        }
    }
}
