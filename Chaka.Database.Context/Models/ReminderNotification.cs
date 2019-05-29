using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class ReminderNotification
    {
        public Guid Uid { get; set; }
        public string RequestType { get; set; }
        public int ReceiverUserId { get; set; }
        public int? TransactionId { get; set; }
        public int OriginatorEmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime NotificationDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public string Icon { get; set; }
        public int BusinessGroupId { get; set; }

        public virtual User ReceiverUser { get; set; }
    }
}
