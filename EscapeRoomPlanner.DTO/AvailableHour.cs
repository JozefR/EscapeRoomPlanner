namespace EscapeRoomPlanner.DTO
{
    public class AvailableHour
    {
        public int Close;
        public int Open;

        public AvailableHour(int open, int close)
        {
            Open = open;
            Close = close;
        }
    }
}