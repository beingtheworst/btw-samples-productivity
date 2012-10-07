using System;

public class GoalId : AbstractIdentity<int>
{
    public GoalId(int Id)
    {
        if (Id < 0) {throw new ArgumentOutOfRangeException("Id")};
        this.Id = Id;
    }
    public override int Id { get; protected set; }
}

public class DailyTaskId : AbstractIdentity<int>
{
    public DailyTaskId(int Id)
    {
        if (Id < 0) {throw new ArgumentOutOfRangeException("Id")};
        this.Id = Id;    }
    public override int Id { get; protected set; }
 
}
