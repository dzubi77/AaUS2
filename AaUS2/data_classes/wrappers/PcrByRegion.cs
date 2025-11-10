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
            if (other == null) return 1;

            int cmp = Test.RegionId.CompareTo(other.Test.RegionId);
            if (cmp != 0) return cmp;

            return Test.TestId.CompareTo(other.Test.TestId);
        }
    }
}
