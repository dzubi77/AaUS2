namespace AaUS2.data_classes.wrappers
{
    /**
     * Wrapper class for PcrTest objects. Compare based on region (secondly by testId).
     */
    public class PcrByRegion(PcrTest test) : IComparable<PcrByRegion>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrByRegion? other)
        {
            throw new NotImplementedException();
        }
    }
}
