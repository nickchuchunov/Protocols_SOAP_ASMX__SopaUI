using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UniversalServichs
{
    [DataContract]
    public class StatisticsService : IStatisticsService
    {
        [DataMember]
        public int SuccessTacts { get; set; }
        [DataMember]
        public int ErrorTacts { get; set; }
        [DataMember]
        public int AllTacts { get; set; }
    }
} 