using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class ActivityHistory
    {
        public int Id { get; set; }
        public int ProcessInstanceId { get; set; }
        public int? ActivityInstanceDestinationId { get; set; }
        public int ReferenceId { get; set; }
        public string ActivityName { get; set; }
        public string ParticipantLoginName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
        public DateTime? ActionDate { get; set; }
        public string WorkflowName { get; set; }
    }
}
