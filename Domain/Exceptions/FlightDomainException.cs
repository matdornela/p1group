using System;

namespace Domain.Exceptions
{
    public class FlightDomainException : Exception
    {
        public FlightDomainException()
        {
        }

        public FlightDomainException(string message) : base(message)
        {
        }
        
        public FlightDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}