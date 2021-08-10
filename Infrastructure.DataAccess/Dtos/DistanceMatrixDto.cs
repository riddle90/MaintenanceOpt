namespace Infrastructure.DataAccess.Dtos
{
    public class DistanceMatrixDto
    {
        public int OriginCustomerId{ get; set; }
        
        public int DestinationCustomerId { get; set; }
        
        public float Distance { get; set; }
        
        public float Time { get; set; }
    }
}