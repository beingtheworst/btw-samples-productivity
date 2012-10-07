
using System;
using System.Collections;
using System.Collections.Generic;
public interface IIdentity
{
    string GetId;
    string GetTag;
}
public abstract class AbstractIdentity<TKey> : IIdentity
{
    private TKey _id;

    public abstract TKey Id { get; protected set; }

    public string GetId() { return Id.ToString(); }
    public string GetTag() { return GetType().ToString().ToLowerInvariant(); }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null,obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

         AbstractIdentity<TKey> identity = obj as AbstractIdentity<TKey>;
         if (identity != null) return (identity.Id.Equals(Id) && string.Equals(identity.GetTag(), GetTag())); ;
         return false;
    }
    public override string ToString()
    {
        return string.Format("{0}-{1}", GetType().Name.Replace("Id", ""), Id);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode(); 
    }
}

public interface ICommand { }
public interface IEvent { }
public interface IApplicationService
{
    void Execute(ICommand command);
}
public interface IEventStore
{
    EventStream LoadEventStream(IIdentity id) ;
    EventStream LoadEventStream(IIdentity id,long skipEvents,int maxCount);
    void AppendToStream(IIdentity id,long expectedVersion,ICollection<IEvent> events);
}


