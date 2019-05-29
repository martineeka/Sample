using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class User
    {
        public User()
        {
            Notifications = new HashSet<Notifications>();
            PasswordAuditTrail = new HashSet<PasswordAuditTrail>();
            PreferenceAuthority = new HashSet<PreferenceAuthority>();
            ReminderNotification = new HashSet<ReminderNotification>();
            UserFunctionAuditTrail = new HashSet<UserFunctionAuditTrail>();
            UserResponsibility = new HashSet<UserResponsibility>();
            UsersImeitransactionDeletedByNavigation = new HashSet<UsersImeitransaction>();
            UsersImeitransactionUser = new HashSet<UsersImeitransaction>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordExpiration { get; set; }
        public int ErrorCounter { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeListFilterId { get; set; }
        public int ResponsibilityGroupId { get; set; }
        public int PreferenceGroupId { get; set; }
        public int RestrictionGroupId { get; set; }
        public int UserStatusId { get; set; }
        public int CurrentBusinessGroupId { get; set; }
        public DateTime? LastLogin { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual UserStatus UserStatus { get; set; }
        public virtual UsersImei UsersImei { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<PasswordAuditTrail> PasswordAuditTrail { get; set; }
        public virtual ICollection<PreferenceAuthority> PreferenceAuthority { get; set; }
        public virtual ICollection<ReminderNotification> ReminderNotification { get; set; }
        public virtual ICollection<UserFunctionAuditTrail> UserFunctionAuditTrail { get; set; }
        public virtual ICollection<UserResponsibility> UserResponsibility { get; set; }
        public virtual ICollection<UsersImeitransaction> UsersImeitransactionDeletedByNavigation { get; set; }
        public virtual ICollection<UsersImeitransaction> UsersImeitransactionUser { get; set; }
    }
}
