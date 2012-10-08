using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.DailyTask
{
    public class DailyTaskAggregate
    {
        public readonly IList<IEvent> Changes = new List<IEvent>();
        readonly DailyTaskState _state;
        public DailyTask(IEnumerable<IEvent> events)
        {
            _state = new DailyTaskState(events);
        }
        public void AssignDailyTask(DailyTaskId id,GoalId goalId,DateTime taskDate,string description)
        {
            if (_state.Created) throw new InvalidOperationException("This Task has already been assigned.");
            Apply(new DailyTaskAssigned(id,goalId,taskDate,description);
        }
        public void CompleteDailyTask(DailyTaskId id,DateTime completedTime)
        {
            if (_state.TaskCompletedOn.HasValue) throw new InvalidOperationException("This Task has already been completed.");
            Apply(new DailyTaskCompleted(id,completedTime);
        }
        public void SetTaskMissed(DailyTaskId id,DateTime taskDate)
        {
            if (_state.TaskCompletedOn.HasValue) throw new InvalidOperationException("This Task has already been completed.");
            if (_state.TaskMissed) throw new InvalidOperationException("This task has already been missed.");
            Apply(new DailyTaskMissed(id,taskDate));
        }
        public void StartTask(DailyTaskId id,DateTime timeStarted)
        {
            if (_state.TaskStartedAt.HasValue) throw new InvalidOperationException("This task has already been started");
            Apply(new DailyTaskStarted(id, timeStarted));
        }
        public void Apply(IEvent e)
        {
            _state.Mutate(e);
            this.Changes.Add(e);
        }

    }
}
