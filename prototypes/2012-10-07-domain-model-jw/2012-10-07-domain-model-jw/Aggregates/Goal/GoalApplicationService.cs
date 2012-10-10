using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.Goal
{
    public class GoalApplicationService : IApplicationService
    {
        IEventStore _eventStore;
        public GoalApplicationService(IEventStore eventStore)
        {
            this._eventStore = eventStore;
        }
        public void Execute(ICommand command)
        {
            ((dynamic)this).Apply((dynamic)command);
        }
        public void When(SetGoal command)
        {
            Update(command.Id, g => g.SetGoal(command.Id, command.Description, command.StartDate, command.LengthOfGoalInDays, command.User));
        }

        public void Update(GoalId Id, Action<GoalAggreagate> execute)
        {
            while (true)
            {

                EventStream eventStream = _eventStore.LoadEventStream(Id);
                GoalAggreagate goal = new GoalAggreagate(eventStream.Events);
                execute(goal);

                try
                {
                    _eventStore.AppendToStream(Id, eventStream.Version, goal.Changes);
                    return;
                }
                catch (OptimisticConcurrencyException ex)
                {
                    foreach (var goalEvent in goal.Changes)
                    {
                        foreach (var actualEvent in ex.ActualEvents)
                        {
                            if (ConflictsWith(goalEvent, actualEvent))
                            {
                                var msg = string.Format("Conflict between {0} and {1}",
                                    goalEvent, actualEvent);
                                throw new RealConcurrencyException(msg, ex);
                            }
                        }
                    }
                    _eventStore.AppendToStream(Id, ex.ActualVersion, goal.Changes);
                }
            }
        }

        static bool ConflictsWith(IEvent x, IEvent y)
        {
            return x.GetType() == y.GetType();
        }
    }
}