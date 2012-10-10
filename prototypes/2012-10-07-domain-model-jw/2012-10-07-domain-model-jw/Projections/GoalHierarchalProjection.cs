using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace _2012_10_07_domain_model_jw.Projections
{
    [DataContract]
    public class GoalHierarchalView
    {
        [DataMember(Order = 1)]
        public GoalId GoalId { get; set; }
        [DataMember(Order = 2)]
        public string Description { get; set; }
        [DataMember(Order = 3)]
        public string User { get; set; }
    }

    public class GoalHierarchalProjection
    {
        IDocumentWriter<GoalId, GoalHierarchalView> _writer;
        public GoalHierarchalProjection(IDocumentWriter<GoalId, GoalHierarchalView> writer)
        {
            _writer = writer;
        }
        public void When(GoalSet e)
        {
            _writer.Add(e.Id, 
                        new GoalHierarchalView { Description = e.Description, 
                                                 GoalId = e.Id, User = e.User });
        }
    }
}
