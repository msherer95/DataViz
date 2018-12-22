using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataViz.Contracts
{
    [DataContract]
    public class QueryPopover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Whether the popover should be visible
        /// </summary>
        [DataMember]
        public bool Toggle { get; set; }

        /// <summary>
        /// Extra columns or values to add to the popover. This should probably
        /// be explored a little more thoroughly. More of a placeholder at the moment.
        /// </summary>
        [DataMember]
        public List<string> Additional { get; set; }
    }
}
