using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Chaka.Api.Workflow.K2Workflow
{

    public class K2StartProcessModel
    {
        public int WorkflowId { get; set; }
        public WorkflowInstance Instance { get; set; }
    }

    [DataContract]
    public class WorkflowInstance
    {
        [DataMember(Name = "expectedDuration")]
        public long ExpectedDuration
        {
            get;
            set;
        }

        [DataMember(Name = "folio")]
        public string Folio
        {
            get;
            set;
        }

        [DataMember(Name = "priority")]
        public long Priority
        {
            get;
            set;
        }

        [DataMember(Name = "xmlFields")]
        public XmlField[] XmlFields
        {
            get;
            set;
        }

        [DataMember(Name = "dataFields")]
        public DataFields DataFields
        {
            get;
            set;
        }

    }

    [DataContract]
    public class XmlField
    {
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "value")]
        public string Value
        {
            get;
            set;
        }
    }


    [DataContract]
    public class DataFields
    {
        // TODO: Define as many fields as needed based on what is in your workflow definition.
        // [DATAFIELDNAME] is the name of a datafield defined within your workflow.

        [DataMember(Name = "ID")]
        public int ID { get; set; }
    }
}
