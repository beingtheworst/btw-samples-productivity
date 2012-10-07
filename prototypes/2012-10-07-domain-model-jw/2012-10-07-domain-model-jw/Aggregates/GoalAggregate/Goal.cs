﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.GoalAggregate
{
    public class Goal
    {
        public readonly IList<IEvent> Changes = new List<IEvent>();
        readonly GoalState _state;
        public Goal(IEnumerable<IEvent> events)
        {
            _state = new GoalState(events);
        }

        public void SetGoal(GoalId id,string description,DateTime startDate, int lengthOfGoalInDays)
        {
            if (_state.Created) throw  new InvalidOperationException("Goal Alread Set");
            Apply(new GoalSet(id,description,startDate,lengthOfGoalInDays));
        }
        public void Apply(IEvent e)
        {
           _state.Mutate(e);
            this.Changes.Add(e);
        }

    }
}
