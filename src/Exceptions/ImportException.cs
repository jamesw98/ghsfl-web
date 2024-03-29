﻿namespace GhsflUtils.Exceptions;

public class ImportException : Exception
{
    public ImportException()
    {
    }
    
    public ImportException(string message) : base(message)
    {
    }
    
    public ImportException(string message, Exception inner) : base(message, inner)
    {
    }
}