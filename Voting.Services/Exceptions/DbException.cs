using System;

namespace Voting.Services.Exceptions
{
    public class DbException<T> : Exception
    {
        public DbException()
        {
            
        }
        
        public DbException(string message) : base(message)
        {
            
        }
    }
}