using System;

namespace Virgin.Shared.Exceptions
{
    /// <summary>
    /// Used as inner exception for exceptions that shouldn't be logged.
    /// </summary>
    public class DoNotLogException : Exception
    {
    }
}