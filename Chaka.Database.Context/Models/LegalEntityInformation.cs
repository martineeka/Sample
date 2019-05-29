using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class LegalEntityInformation
    {
        public LegalEntityInformation()
        {
            OrgUnit = new HashSet<OrgUnit>();
            OrgUnitTransactionDetail = new HashSet<OrgUnitTransactionDetail>();
        }

        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int ManagingDirectorId { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string FaxNumber1 { get; set; }
        public string FaxNumber2 { get; set; }
        public string Address { get; set; }
        public string TaxFileNumber { get; set; }
        public DateTime TfnregisterDate { get; set; }
        public int KppbranchId { get; set; }
        public string Sppkpnumber { get; set; }
        public DateTime SppkpreleaseDate { get; set; }
        public int BusinessFieldRegulationId { get; set; }
        public int BusinessFieldCategoryId { get; set; }
        public int BusinessFieldClassificationId { get; set; }
        public int CompanyBrandId { get; set; }
        public int FundingId { get; set; }
        public int BusinessStatusOwnershipId { get; set; }
        public int TaxDutyId { get; set; }
        public string TaxInvoiceSerialCode { get; set; }
        public string Siupnumber { get; set; }
        public int? SiupclassId { get; set; }
        public DateTime? SiupbeginEff { get; set; }
        public DateTime? SiuplastEff { get; set; }
        public decimal? SiupinitialAsset { get; set; }
        public string SiupbusinessInstitutional { get; set; }
        public string SiupgoodsOrService { get; set; }
        public string Tdpnumber { get; set; }
        public DateTime TdpbeginEff { get; set; }
        public DateTime TdplastEff { get; set; }
        public string Situnumber { get; set; }
        public DateTime? SitubeginEff { get; set; }
        public DateTime? SitulastEff { get; set; }
        public decimal? SitubusinessArea { get; set; }
        public int? AttachmentId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual BusinessFieldCategory BusinessFieldCategory { get; set; }
        public virtual BusinessFieldClassification BusinessFieldClassification { get; set; }
        public virtual BusinessFieldRegulation BusinessFieldRegulation { get; set; }
        public virtual BusinessStatusOwnership BusinessStatusOwnership { get; set; }
        public virtual CompanyBrand CompanyBrand { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Funding Funding { get; set; }
        public virtual KppBranch Kppbranch { get; set; }
        public virtual Employee ManagingDirector { get; set; }
        public virtual Siupclass1 Siupclass { get; set; }
        public virtual TaxDuty TaxDuty { get; set; }
        public virtual ICollection<OrgUnit> OrgUnit { get; set; }
        public virtual ICollection<OrgUnitTransactionDetail> OrgUnitTransactionDetail { get; set; }
    }
}
