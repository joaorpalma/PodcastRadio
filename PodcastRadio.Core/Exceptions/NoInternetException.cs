using System;
namespace PodcastRadio.Core.Exceptions
{
    public class NoInternetException : Exception
    {
        public NoInternetException() {}
        public NoInternetException(string message) : base(message) {}
        public NoInternetException(string message, Exception inner) : base(message, inner) {}
    }
}
