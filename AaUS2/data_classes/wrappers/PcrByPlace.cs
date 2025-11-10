namespace AaUS2.data_classes.wrappers
{
    public class PcrByPlace(PcrTest test) : IComparable<PcrByPlace>
    {
        public PcrTest Test { get; } = test;

        public int CompareTo(PcrByPlace? other)
        {
            if (other == null) return 1;

            int placeComparison = Test.PlaceId.CompareTo(other.Test.PlaceId);
            if (placeComparison != 0) return placeComparison;
            
            return Test.TestId.CompareTo(other.Test.TestId);
        }
    }
}
