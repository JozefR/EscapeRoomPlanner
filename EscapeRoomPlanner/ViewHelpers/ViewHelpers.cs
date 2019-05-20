namespace EscapeRoomPlanner.ViewHelpers
{
    public static class ViewHelpers
    {
        public static string intToTime(this int hour)
        {
            var stringHour = "";

            if (hour < 10)
                stringHour += $"0{hour}:00";
            else
                stringHour += $"{hour}:00";

            return stringHour;
        }
    }
}