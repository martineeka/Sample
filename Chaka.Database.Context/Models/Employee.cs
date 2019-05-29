using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InversePreviousEmployee = new HashSet<Employee>();
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
            OrgUnit = new HashSet<OrgUnit>();
            OrgUnitTransactionDetail = new HashSet<OrgUnitTransactionDetail>();
        }

        public int Id { get; set; }
        public string Nik { get; set; }
        public string FingerPrintCode { get; set; }
        public int? PreviousEmployeeId { get; set; }
        public int? GradeId { get; set; }
        public string Photo { get; set; }
        public bool IsProbation { get; set; }
        public DateTime ProbationPeriodStart { get; set; }
        public DateTime ProbationPeriodEnd { get; set; }
        public DateTime? FinalProcessDate { get; set; }
        public DateTime HiredDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public bool IsBlackList { get; set; }
        public bool ContinueTaxes { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Employee PreviousEmployee { get; set; }
        public virtual ICollection<Employee> InversePreviousEmployee { get; set; }
        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
        public virtual ICollection<OrgUnit> OrgUnit { get; set; }
        public virtual ICollection<OrgUnitTransactionDetail> OrgUnitTransactionDetail { get; set; }
    }
}
