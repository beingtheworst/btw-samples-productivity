﻿using _2012_10_07_domain_model_jw.Projections;
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
        UserGoalIndex goalService;

        public DailyTaskAggregate(IEnumerable<IEvent> events, UserGoalIndex goalService)
        {
            _state = new DailyTaskState(events);
            this.goalService = goalService;
        }
        public void AssignDailyTask(DailyTaskId id,GoalId goalId,DateTime taskDate,string description, string user)
        {
            if (!goalService.HasGoalBeenCreated(goalId)) throw new InvalidOperationException("Goal Has not been created");
            if (_state.Created) throw new InvalidOperationException("This Task has already been assigned.");
            if (!goalService.IsValidUser(goalId, user)) throw new InvalidOperationException("You are not not assign tasks to this goal.");

            Apply(new DailyTaskScheduled(id,goalId,taskDate,description, user));
        }
        public void CompleteDailyTask(DailyTaskId id,DateTime completedTime)
        {
            if (_state.TaskCompletedOn.HasValue) throw new InvalidOperationException("This Task has already been completed.");
            if (!_state.TaskStartedAt.HasValue) throw new InvalidOperationException("This Task has not been started.");
            Apply(new DailyTaskCompleted(id,completedTime));
        }
        public void SetTaskMissed(DailyTaskId id,DateTime taskDate)
        {
            if (_state.TaskCompletedOn.HasValue) throw new InvalidOperationException("This Task has already been completed.");
            if (_state.TaskMissed) throw new InvalidOperationException("This task has already been missed.");
            if (_state.TaskStartedAt.HasValue) throw new InvalidOperationException("This Task has already been started.");
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