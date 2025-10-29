using AaUS2.structures;

namespace AaUS2.data_classes
{
    public class PcrTest(int testId, int placeId, int regionId, int districtId, bool isPositive, double resultValue, string patientId, string note, DateTime testDateTime)
    {
        public int TestId { get; set; } = testId;
        public int PlaceId { get; set; } = placeId;
        public int RegionId { get; set; } = regionId;
        public int DistrictId { get; set; } = districtId;
        public bool IsPositive { get; set; } = isPositive;
        public double ResultValue { get; set; } = resultValue;
        public required string PatientId { get; set; } = patientId;
        public required string Note { get; set; } = note;
        public DateTime TestDateTime { get; set; } = testDateTime;

        public string ToString()
        {
            return "";
        }
    }
}
