using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DataViz.Contracts
{
    [DataContract]
    public class QueryCategories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Qualitative columns that can be used as categories by themselves,
        /// without specifying a condition.
        /// E.g. gender, color, etc...
        /// </summary>
        [DataMember]
        public List<string> Columns { get; set; }

        /// <summary>
        /// Maps column name to the condition
        /// E.g. {'Age', '> 10'}
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Conditionals { get; set; }
    }
}
