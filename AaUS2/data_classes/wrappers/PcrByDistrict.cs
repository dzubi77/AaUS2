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
            if (other == null) return 1;

            int cmp = Test.DistrictId.CompareTo(other.Test.DistrictId);
            if (cmp != 0) return cmp;

            return Test.TestId.CompareTo(other.Test.TestId);
        }
    }
}
