using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataViz.Contracts
{
    [DataContract]
    public class TableResult
    {
        [DataMember]
        public List<string> Columns { get; set; }

        [DataMember]
        public List<List<object>> Rows { get; set; }

        [DataMember]
        public int ResultCount { get; set; }

        [DataMember]
        public Dictionary<int, string> ColTypeByNumber { get; set; }
    }
}
