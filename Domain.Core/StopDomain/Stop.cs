using System;
using Domain.Core.Common;

namespace Domain.Core
{
    public class Stop
    {
        public Guid Guid { get; }
        public int CustomerId { get; }
        public string Address { get; }
        public string Zipcode { get; }
        public string City { get; }
        public TimeWindow TimeWindow { get; }
        public int StopTime { get; }

        public Stop(Guid guid, int customerId, string address, string zipcode, string city, TimeWindow timeWindow, int stopTime)
        {
            Guid = guid;
            CustomerId = customerId;
            Address = address;
            Zipcode = zipcode;
            City = city;
            TimeWindow = timeWindow;
            StopTime = stopTime;
        }
    }
}