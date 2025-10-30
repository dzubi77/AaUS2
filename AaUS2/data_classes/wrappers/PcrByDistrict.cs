namespace AaUS2.data_classes.wrappers
{
    /**
     * Wrapper class for PcrTest objects. Compare based on district (secondly by testId).
     */
    public class PcrByDistrict(PcrTest test) : IComparable<PcrByDistrict>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrByDistrict? other)
        {
            throw new NotImplementedException();
        }
    }
}
