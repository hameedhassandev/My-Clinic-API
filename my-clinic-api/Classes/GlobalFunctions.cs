namespace my_clinic_api.Classes
{
    public class GlobalFunctions
    {
        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime start = DateTime.Now.Date;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
