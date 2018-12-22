using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataViz.Contracts
{
    [DataContract]
    public class QueryRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string TableName { get; set; }

        [DataMember]
        public string XCol { get; set; }

        /// <summary>
        /// Any Y columns that should be included, can be multiple.
        /// </summary>
        [DataMember]
        public List<string> YCols { get; set; }

        [DataMember]
        public int Skip { get; set; }

        [DataMember]
        public int Take { get; set; }

        [DataMember]
        public string GraphType { get; set; }

        [DataMember]
        public string GraphSubType { get; set; }

        /// <summary>
        /// A WHERE clause
        /// </summary>
        [DataMember]
        public string Filters { get; set; }

        /// <summary>
        /// Any categories that should be calculated to use
        /// for further differentiation.
        /// </summary>
        [DataMember]
        public virtual QueryCategories Categories { get; set; }

        /// <summary>
        /// Maps function name to the function.
        /// E.g. {"mySum", "x1 + y1"}
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Functions { get; set; }

        /// <summary>
        /// Describes how the hover popover should look
        /// </summary>
        [DataMember]
        public virtual QueryPopover Popover { get; set; }

        /// <summary>
        /// Appearance specifications. This will get it's own type when we get
        /// to it. Right now, this is just a placeholder.
        /// </summary>
        [DataMember]
        public string Appearance { get; set; }
    }
}
