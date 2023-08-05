using System;

namespace CarRentalManagerAPI.Exceptions
{
    public class ValueIsTakenException : Exception
    {
        public ValueIsTakenException(string message) : base(message)
        {
            
        }
    }
}
