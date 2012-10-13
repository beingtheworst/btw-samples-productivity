using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace _2012_10_07_domain_model_jw.Projections
{
    [DataContract]
    public class UserGoalsView
    {
        [DataMember(Order = 1)]
        public string User { get; set; }
        [DataMember(Order = 2)]
        public IList<GoalId> Goals { get; set; }

        public UserGoalsView()
        {
            this.Goals = new List<GoalId>();
        }
    }

    public class UserGoalsProjection
    {
        IDocumentWriter<string, UserGoalsView> _writer;
        public UserGoalsProjection(IDocumentWriter<string, UserGoalsView> writer)
        {
            _writer = writer;
        }

        public void When(GoalSet e)
        {
           Func<UserGoalsView> CreateView;
            CreateView = () => {
                                    var view = new UserGoalsView();
                                    view.Goals.Add(e.Id);
                                    view.User = e.User;
                                    return view;
                                };

            _writer.AddOrUpdate(e.User,CreateView(),g=> g.Goals.Add(e.Id));

        }
    }
}
