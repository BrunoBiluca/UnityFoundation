using System;
using System.Runtime.Serialization;

[Serializable]
public class DestroyPooledObjectException : Exception
{
    public DestroyPooledObjectException(){ }

    public DestroyPooledObjectException(string message) : base(message) { }

    public DestroyPooledObjectException(string message, Exception innerException) 
        : base(message, innerException) {}

    protected DestroyPooledObjectException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }
}