using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.Goal
{
    public class GoalAggreagate
    {
        public readonly IList<IEvent> Changes = new List<IEvent>();
        readonly GoalState _state;
        public GoalAggreagate(IEnumerable<IEvent> events)
        {
            _state = new GoalState(events);
        }

        public void SetGoal(GoalId id, string description, DateTime startDate, int lengthOfGoalInDays, string user)
        {
            if (_state.Created) throw new InvalidOperationException("Goal Already Set");
            Apply(new GoalSet(id, description, startDate, lengthOfGoalInDays,user));
        }
        public void Apply(IEvent e)
        {
            _state.Mutate(e);
            this.Changes.Add(e);
        }

    }
}