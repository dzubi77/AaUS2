namespace AaUS2.data_classes.wrappers
{
    public class PcrById(PcrTest test) : IComparable<PcrById>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrById? other)
        {
            throw new NotImplementedException();
        }
    }
}
