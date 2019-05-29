using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Attachment
    {
        public Attachment()
        {
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
        }

        public int Id { get; set; }
        public int TableOriginId { get; set; }
        public string TableOriginName { get; set; }
        public string AttachmentFile { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
    }
}
