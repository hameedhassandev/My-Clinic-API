namespace my_clinic_api.Classes
{
    public class GlobalFunctions
    {
        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Now.Date;
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
    }
}
