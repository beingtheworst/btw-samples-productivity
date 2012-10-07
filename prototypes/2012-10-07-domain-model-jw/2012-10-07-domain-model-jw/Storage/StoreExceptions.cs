using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class OptimisticConcurrencyException : Exception
{
    public long ActualVersion { get; private set; }
    public long ExpectedVersion { get; private set; }
    public IIdentity Id { get; private set; }
    public IList<IEvent> ActualEvents { get; private set; }

    OptimisticConcurrencyException(string message, long actualVersion, long expectedVersion, IIdentity id,
        IList<IEvent> serverEvents)
        : base(message)
    {
        ActualVersion = actualVersion;
        ExpectedVersion = expectedVersion;
        Id = id;
        ActualEvents = serverEvents;
    }

    public static OptimisticConcurrencyException Create(long actual, long expected, IIdentity id,
        IList<IEvent> serverEvents)
    {
        var message = string.Format("Expected v{0} but found v{1} in stream '{2}'", expected, actual, id);
        return new OptimisticConcurrencyException(message, actual, expected, id, serverEvents);
    }

    protected OptimisticConcurrencyException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context) { }
}


[Serializable]
public class RealConcurrencyException : Exception
{
    public RealConcurrencyException() { }
    public RealConcurrencyException(string message) : base(message) { }
    public RealConcurrencyException(string message, Exception inner) : base(message, inner) { }

    protected RealConcurrencyException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context) { }
}