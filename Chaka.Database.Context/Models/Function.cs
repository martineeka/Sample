using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Function
    {
        public Function()
        {
            MenuDetail = new HashSet<MenuDetail>();
            PreferenceAuthority = new HashSet<PreferenceAuthority>();
            UserFunction = new HashSet<UserFunction>();
            UserFunctionAuditTrail = new HashSet<UserFunctionAuditTrail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DescriptionIna { get; set; }
        public string FunctionView { get; set; }
        public string IconUrl { get; set; }
        public int? BusinessTemplateId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
        public string DescriptionEng { get; set; }

        public virtual ICollection<MenuDetail> MenuDetail { get; set; }
        public virtual ICollection<PreferenceAuthority> PreferenceAuthority { get; set; }
        public virtual ICollection<UserFunction> UserFunction { get; set; }
        public virtual ICollection<UserFunctionAuditTrail> UserFunctionAuditTrail { get; set; }
    }
}
