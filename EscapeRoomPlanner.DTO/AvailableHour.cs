namespace EscapeRoomPlanner.DTO
{
    public class AvailableHour
    {
        public int Open;
        public int Close;

        public AvailableHour(int open, int close)
        {
            Open = open;
            Close = close;
        }
    }
}