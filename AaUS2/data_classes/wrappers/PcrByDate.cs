namespace AaUS2.data_classes.wrappers
{
    public class PcrByDate(PcrTest test) : IComparable<PcrByDate>
    {
        public PcrTest Test { get; } = test;


        public int CompareTo(PcrByDate? other)
        {
            if (other == null) return 1;

            int cmp = Test.TestDateTime!.Value.CompareTo(other.Test.TestDateTime!.Value);
            if (cmp != 0) return cmp;

            return Test.TestId.CompareTo(other.Test.TestId);
        }
    }
}
