namespace AaUS2.data_classes
{
    /**
     * Objects representing PCR tests. Operation CompareTo is defined in each wrapper class.
     */
    public class PcrTest(int testId, int placeId, int regionId, int districtId, bool isPositive, double resultValue, string patientId, string note, DateTime? testDateTime)
    {
        public int TestId { get; } = testId;
        public int PlaceId { get; } = placeId;
        public int RegionId { get; } = regionId;
        public int DistrictId { get; } = districtId;
        public bool IsPositive { get; } = isPositive;
        public double ResultValue { get; } = resultValue;
        public string PatientId { get; } = patientId;
        public string Note { get; } = note;
        public DateTime? TestDateTime { get; } = testDateTime;

        public PcrTest(int testId) : this(testId, 0, 0, 0, false, 0, "", "", new DateTime()) { }
         
        // dummy constructor by testId
        public PcrTest(int testId, DateTime dateTime) : this(testId, 0, 0, 0, false, 0, "", "", dateTime) {}
        
        // dummy constructor by districtId
        public PcrTest(int testId, int districtId, DateTime dateTime) 
            : this(testId, 0, 0, districtId, false, 0, "", "", dateTime) {}

        // dummy constructor by regionId
        public PcrTest(int testId, int regionId, DateTime dateTime, bool isRegion)
            : this(testId, 0, regionId, 0, false, 0, "", "", dateTime) {}

        // dummy constructor by regionId
        public PcrTest(int testId, int placeId, DateTime dateTime, int isByPlace)
            : this(testId, placeId, 0, 0, false, 0, "", "", dateTime) {}

        //------------------

        /**
         * Returns string that could be used to save in csv.
         */
        public override string ToString() 
        {
            return TestId + ";" + PlaceId + ";" + RegionId + ";" + DistrictId + ";" + IsPositive + ";" + ResultValue + ";" + PatientId + ";" + Note + ";" + TestDateTime.ToString();
        }
    }
}
