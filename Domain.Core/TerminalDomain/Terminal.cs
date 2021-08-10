namespace Domain.Core.TerminalDomain
{
    public class Terminal
    {
        public long CustomerId { get; }
        public string Address { get; }
        public string City { get; }
        public string Zip { get; }
        public bool IsDummy { get; }

        public Terminal(long customerId, string address, string city, string zip, bool isDummy)
        {
            CustomerId = customerId;
            Address = address;
            City = city;
            Zip = zip;
            IsDummy = isDummy;
        }
    }
}