using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace DataViz.Contracts
{
    [DataContract]
    public class QueryPopover
    {
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
