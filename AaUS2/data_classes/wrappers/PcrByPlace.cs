namespace AaUS2.data_classes.wrappers
{
    public class PcrByPlace(PcrTest test) : IComparable<PcrByPlace>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrByPlace? other)
        {
            throw new NotImplementedException();
        }
    }
}
