using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace _2012_10_07_domain_model_jw.Projections
{
    [DataContract]
    public class GoalCalendarView
    {
        [DataMember(Order = 1)]
        public string User { get; set; }
        [DataMember(Order = 2)]
        public GoalId GoalId { get; set; }
        [DataMember(Order=3)]
        public DateTime DateStarted {get;set;}
        [DataMember(Order=4)]
        public string GoalDescription {get;set;}
        [DataMember(Order = 5)]
        public Dictionary<DailyTaskId,Day> Days { get; set; }

        public GoalCalendarView()
        {
            this.Days = new Dictionary<DailyTaskId, Day>();
        }
    }

    public class Day
    {
        
        public DateTime TaskDate { get;  set; }
        public DateTime? TaskCompletedOn { get;  set; }
        public bool TaskMissed { get;  set; }
        public DateTime? TaskStartedAt { get;  set; }
        public string Description { get;  set; }
    }
    public class GoalCalendarViewProjections
    {
        IDocumentWriter<GoalId,GoalCalendarView> _writer;
        public GoalCalendarViewProjections(IDocumentWriter<GoalId,GoalCalendarView> writer)
        {
            _writer = writer;
        }
        public void When(GoalSet e)
        {
            _writer.Add(e.Id, 
                        new GoalCalendarView { GoalId = e.Id, 
                                               DateStarted = e.StartDate, 
                                               GoalDescription = e.Description, 
                                               User = e.User });

        }
        public void When(DailyTaskScheduled e)
        {
            _writer.UpdateEnforcingNew(e.GoalId, g =>
                {
                    Day day;
                    if (g.Days.ContainsKey(e.Id))
                    {
                        g.Days.TryGetValue(e.Id, out day);
                        day.Description = e.Description;
                        day.TaskDate = e.TaskDate;
                    }
                    else
                    {
                        g.Days.Add(e.Id, new Day()
                        {
                            Description = e.Description,
                            TaskDate = e.TaskDate
                        });
                    }
                });

        }
    }
}
