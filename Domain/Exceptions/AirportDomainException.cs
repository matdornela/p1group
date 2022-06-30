using System;

namespace Domain.Exceptions
{
    public class AirportDomainException : Exception
    {
        public AirportDomainException()
        {
        }

        public AirportDomainException(string message) : base(message)
        {
        }
        
        public AirportDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}