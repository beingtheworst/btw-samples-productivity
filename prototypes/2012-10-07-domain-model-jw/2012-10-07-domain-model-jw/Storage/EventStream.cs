using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class EventStream
    {
        public long Version;
        public List<IEvent> Events = new List<IEvent>();
    }

