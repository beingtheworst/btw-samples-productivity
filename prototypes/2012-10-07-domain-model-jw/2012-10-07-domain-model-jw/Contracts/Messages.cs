using System;
using System.Runtime.Serialization;

[Serializable]
[DataContract(Namespace="DOMAIN")]
public class GoalSet : IEvent
{
    public GoalSet(GoalId Id, string description, DateTime startDate, int lengthOfGoalInDays, string user)
    {
        this.Id = Id;
        this.Description = description;
        this.StartDate = startDate;
        this.LengthOfGoalInDays = lengthOfGoalInDays;
        this.User = user;
    }
    [DataMember(Order=1)]
    public GoalId Id { get; private set; }
    [DataMember(Order=2)]
    public string Description { get; private set; }
    [DataMember(Order = 3)]
    public DateTime StartDate { get; private set; }
    [DataMember(Order = 4)]
    public int LengthOfGoalInDays { get; private set; }
    [DataMember(Order = 5)]
    public string User { get; private set; }

}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class SetGoal : ICommand
{
    public SetGoal(GoalId Id, string description, DateTime startDate, int lengthOfGoalInDays, string user)
    {
        this.Id = Id;
        this.Description = description;
        this.StartDate = startDate;
        this.LengthOfGoalInDays = lengthOfGoalInDays;
        this.User = user;
    }
    [DataMember(Order = 1)]
    public GoalId Id { get; private set; }
    [DataMember(Order = 2)]
    public string Description { get; private set; }
    [DataMember(Order = 3)]
    public DateTime StartDate { get; private set; }
    [DataMember(Order = 4)]
    public int LengthOfGoalInDays { get; private set; }
    [DataMember(Order = 5)]
    public string User { get; private set; }
}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class DailyTaskScheduled : IEvent
{
    public DailyTaskScheduled(DailyTaskId id, GoalId goalId, DateTime taskDate, string description, string user)
    {
        this.Id = id;
        this.GoalId = goalId;
        this.TaskDate = taskDate;
        this.Description = description;
        this.User = user;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public GoalId GoalId { get; private set; }
    [DataMember(Order = 3)]
    public DateTime TaskDate { get; private set; }
    [DataMember(Order = 4)]
    public string Description { get; private set; }
    [DataMember(Order = 5)]
    public string User { get; private set; }
}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class ScheduleDailyTask : ICommand
{
    public ScheduleDailyTask(DailyTaskId id, GoalId goalId, DateTime taskDate, string description, string user)
    {
        this.Id = id;
        this.GoalId = goalId;
        this.TaskDate = taskDate;
        this.Description = description;
        this.User = user;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public GoalId GoalId { get; private set; }
    [DataMember(Order = 3)]
    public DateTime TaskDate { get; private set; }
    [DataMember(Order = 4)]
    public string Description { get; private set; }
    [DataMember(Order = 5)]
    public string User { get; private set; }
}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class DailyTaskCompleted : IEvent
{
    public DailyTaskCompleted(DailyTaskId id, DateTime completeTime)
    {
        this.Id = id;
        this.CompleteTime = completeTime;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public DateTime CompleteTime { get; private set; }
}
[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class SetDailyTaskToComplete : ICommand
{
    public SetDailyTaskToComplete(DailyTaskId id,DateTime completeTime)
    {
        this.Id = id;
        this.CompleteTime = completeTime;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public DateTime CompleteTime { get; private set; }
}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class DailyTaskStarted : IEvent
{
    public DailyTaskStarted(DailyTaskId id, DateTime startTime)
    {
        this.Id = id;
        this.StartTime = startTime;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public DateTime StartTime { get; private set; }
}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class StartDailyTask : ICommand
{
    public StartDailyTask(DailyTaskId id, DateTime startTime)
    {
        this.Id = id;
        this.StartTime = startTime;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public DateTime StartTime { get; private set; }
}

[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class DailyTaskMissed : IEvent
{
    public DailyTaskMissed(DailyTaskId id, DateTime taskDate)
    {
        this.Id = id;
        this.TaskDate = taskDate;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public DateTime TaskDate { get; private set; }
}
[Serializable]
[DataContract(Namespace = "DOMAIN")]
public class RecordDailyTaskMissed : IEvent
{
    public RecordDailyTaskMissed(DailyTaskId id, DateTime taskDate)
    {
        this.Id = id;
        this.TaskDate = taskDate;
    }
    [DataMember(Order = 1)]
    public DailyTaskId Id { get; private set; }
    [DataMember(Order = 2)]
    public DateTime TaskDate { get; private set; }
}
