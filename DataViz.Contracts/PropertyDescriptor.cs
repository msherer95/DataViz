using System;
using System.Runtime.Serialization;

namespace DataViz.Contracts
{
    [DataContract]
    public class PropertyDescriptor
    {
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The type that will be stored in the DB (VARCHAR, NUMERIC, etc...)
        /// </summary>
        [DataMember]
        public string DbType { get; set; }
    }
}
