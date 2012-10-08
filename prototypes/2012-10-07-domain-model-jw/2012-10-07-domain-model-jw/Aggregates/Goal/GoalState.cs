using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.Goal
{
    public class GoalState
    {
        public GoalState(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                Mutate(@event);
            }

        }
        public bool Created { get; private set; }
        public GoalId Id { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public int LengthOfGoalInDays { get; private set; }

        public void Mutate(IEvent @event)
        {
            ((dynamic)this).When((dynamic)@event);
        }
        public void When(GoalSet e)
        {
            this.Created = true;
            this.Description = e.Description;
            this.Id = e.Id;
            this.LengthOfGoalInDays = e.LengthOfGoalInDays;
            this.StartDate = e.StartDate;
        }
    }
}
