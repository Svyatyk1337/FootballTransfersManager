using System;
using System.Collections.Generic;

namespace FootballTransfers.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(string message, IDictionary<string, string[]>? errors = null) : base(message)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}
