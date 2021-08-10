namespace Domain.Core.LocationDomain
{
    public class DistanceInfo
    {
        public DistanceInfo(float distance, float time)
        {
            this.Distance = distance;
            this.Time = time;
        }

        public float Time { get; set; }

        public float Distance { get; set; }
    }
}