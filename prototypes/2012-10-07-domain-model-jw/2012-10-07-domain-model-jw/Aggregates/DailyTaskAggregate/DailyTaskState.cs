using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.DailyTaskAggregate
{
    public class DailyTaskState
    {

        public DailyTaskId Id { get; private set; }
        public GoalId GoalId { get; private set; }
        public DateTime TaskDate { get; private set; }
        public DateTime? TaskCompletedOn { get; private set; }
        public bool TaskMissed { get; private set; }
        public DateTime? TaskStartedAt { get; private set; }
        public string Description { get; private set; }
        public bool Created { get; private set; }

        public DailyTaskState(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                Mutate(@event);
            }

        }


        public void Mutate(IEvent @event)
        {
            ((dynamic)this).When((dynamic)@event);
        }
        public void When(DailyTaskAssigned e)
        {
            this.Id = e.Id;
            this.GoalId = e.GoalId;
            this.TaskDate = e.TaskDate;
            this.Description = e.Description;
            this.Created = true;
        }

        public void When(DailyTaskCompleted e)
        {
            this.TaskCompletedOn = e.CompleteTime;
        }
        public void When(DailyTaskMissed e)
        {
            this.TaskMissed = true;
        }
        public void When(DailyTaskStarted e)
        {
            this.TaskStartedAt = e.StartTime;
        }
    }
}