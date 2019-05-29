using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Notifications
    {
        public Guid Uid { get; set; }
        public string RequestType { get; set; }
        public int RequesterId { get; set; }
        public DateTime? ReadDate { get; set; }
        public string SerialNumber { get; set; }
        public int TransactionId { get; set; }
        public string Icon { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public int BusinessGroupId { get; set; }

        public virtual User Requester { get; set; }
    }
}
