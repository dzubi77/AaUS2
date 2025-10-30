namespace AaUS2.data_classes.wrappers
{
    public class PcrByDate(PcrTest test) : IComparable<PcrByDate>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrByDate? other)
        {
            throw new NotImplementedException();
        }
    }
}
