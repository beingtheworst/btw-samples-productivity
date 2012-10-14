using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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

    }

    public class GoalCalendareViewProjections
    {
        IDocumentWriter<GoalId,GoalCalendarView> _writer;
        public GoalCalendareViewProjections(IDocumentWriter<GoalId,GoalCalendarView> writer)
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

        }
    }
}
