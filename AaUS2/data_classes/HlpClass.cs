namespace AaUS2.data_classes
{
    public class HlpClass
    {
        public static string DateToString(DateOnly date)
        {
            return date.Day + "/" + date.Month + "/" + date.Year;
        }
    }
}
