using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2012_10_07_domain_model_jw.Aggregates.DailyTask
{
    public class DailyTaskApplicationService : IApplicationService
    {
        IEventStore _eventStore;
        public DailyTaskApplicationService(IEventStore eventStore)
        {
            this._eventStore = eventStore;
        }

        public void Execute(ICommand command)
        {
            ((dynamic)this).Apply((dynamic)command);
        }
        public void When(AssignDailyTask c)
        {
            Update(c.Id, a => a.AssignDailyTask(c.Id,
                                              c.GoalId,
                                              c.TaskDate,
                                              c.Description));
        }
        public void When(SetDailyTaskToComplete c)
        {
            Update(c.Id, a => a.CompleteDailyTask(c.Id,
                                                  c.CompleteTime));
        }
        public void When(RecordDailyTaskMissed c)
        {
            Update(c.Id, a => a.SetTaskMissed(c.Id,
                                              c.TaskDate));
        }
        public void When(StartDailyTask c)
        {
            Update(c.Id, a => a.StartTask(c.Id,
                                          c.StartTime));
        }

        public void Update(DailyTaskId Id, Action<DailyTaskAggregate> execute)
        {
            while (true)
            {

                EventStream eventStream = _eventStore.LoadEventStream(Id);
                DailyTaskAggregate dailyTask = new DailyTaskAggregate(eventStream.Events);
                execute(dailyTask);

                try
                {
                    _eventStore.AppendToStream(Id, eventStream.Version, dailyTask.Changes);
                    return;
                }
                catch (OptimisticConcurrencyException ex)
                {
                    foreach (var dailyTaskEvent in dailyTask.Changes)
                    {
                        foreach (var actualEvent in ex.ActualEvents)
                        {
                            if (ConflictsWith(dailyTaskEvent, actualEvent))
                            {
                                var msg = string.Format("Conflict between {0} and {1}",
                                    dailyTaskEvent, actualEvent);
                                throw new RealConcurrencyException(msg, ex);
                            }
                        }
                    }
                    _eventStore.AppendToStream(Id, ex.ActualVersion, dailyTask.Changes);
                }
            }
        }

        static bool ConflictsWith(IEvent x, IEvent y)
        {
            return x.GetType() == y.GetType();
        }
    }
}