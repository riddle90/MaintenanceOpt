namespace Domain.Core.Common
{
    public class TimeWindow
    {
        public int StartTime { get; }
        public int EndTime { get; }

        public TimeWindow(int startTime, int endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}