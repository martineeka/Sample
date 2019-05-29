using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
   public class CreateEditViewModel
    {
        #region org unit

        public string ID { get; set; } //(int, not null)

        public string headerID { get; set; }


        [Required]
        [StringLength(10, ErrorMessage = "Maximum of 10 characters")]
        public string Code { get; set; } //(varchar(5), null)

        [Required]
        [Display(Name = "Cost Center")]
        public int? CostCenterID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string Name { get; set; } //(varchar(50), not null)

        [Required]
        [Display(Name = "Organization Level")]
        public int OrganizationlevelID { get; set; } //(int, not null)

        public string SuperiorID { get; set; }

        [Display(Name = "Superior")]
        public string SuperiorCode { get; set; }

        [Display(Name = "Superior")]
        public string SuperiorName { get; set; }


        [StringLength(500, ErrorMessage = "Maximum of 500 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Begin Effective")]
        public DateTime? BeginEff { get; set; }

        [Display(Name = "Last Effective")]
        public DateTime? LastEff { get; set; }

        [Display(Name = "Category")]
       
        public int CategoryID { get; set; }

        [Display(Name = "Parent Organization")]
        public string ParentName { get; set; }
        public string ParentID { get; set; } //(int, null)    

        [Required]
        [Display(Name = "Legal Entity")]
        public bool IsLegalEntity { get; set; }

        public int? LegalEntityInformationID { get; set; }

        #endregion

        #region legal entity

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }

        [Required]
        [Display(Name = "Managing Director")]
        public string ManagingDirectorID { get; set; }

        [Required]
        [Display(Name = "Managing Director")]
        public string ManagingDirectorCode { get; set; }

        public string ManagingDirectorName { get; set; }

        [Display(Name = "Phone Number 1")]
        [StringLength(15, ErrorMessage = "Maximum of 15 characters")]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "Phone Number 2")]
        [StringLength(15, ErrorMessage = "Maximum of 15 characters")]
        public string PhoneNumber2 { get; set; }

        [Display(Name = "Fax Number 1")]
        [StringLength(15, ErrorMessage = "Maximum of 15 characters")]
        public string FaxNumber1 { get; set; }

        [Display(Name = "Fax Number 2")]
        [StringLength(15, ErrorMessage = "Maximum of 15 characters")]
        public string FaxNumber2 { get; set; }

        [StringLength(200, ErrorMessage = "Maximum of 200 characters")]
        public string Address { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        [Display(Name = "Tax File Number")]
        public string TaxFileNumber { get; set; }

        [Required]
        [Display(Name = "Register Date")]
        public DateTime TFNRegisterDate { get; set; }

        [Required]
        [Display(Name = "KPP Branch")]
        public int KPPBranchID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        public string SPPKPNumber { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime SPPKPReleaseDate { get; set; }

        [Required]
        [Display(Name = "Business Field Regulation")]
        public int BusinessFieldRegulationID { get; set; }

        [Required]
        [Display(Name = "Business Field Category")]
        public int BusinessFieldCategoryID { get; set; }

        [Required]
        [Display(Name = "Business Field Classification")]
        public int BusinessFieldClassificationID { get; set; }

        [Required]
        [Display(Name = "Company Brand")]
        public int CompanyBrandID { get; set; }

        [Required]
        [Display(Name = "Funding")]
        public int FundingID { get; set; }

        [Required]
        [Display(Name = "Business Status")]
        public int BusinessStatusOwnershipID { get; set; }

        [Required]
        [Display(Name = "Tax Duty")]
        public int TaxDutyID { get; set; }

        [Required]
        [Display(Name = "Tax Invoice Serial Code")]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string TaxInvoiceSerialCode { get; set; }

        [Display(Name = "SIUP Number")]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        public string SIUPNumber { get; set; }

        [Display(Name = "Class")]
        public int? SIUPClassID { get; set; }

        [Display(Name = "Begin Effective")]
        public DateTime? SIUPBeginEff { get; set; }

        [Display(Name = "Last Effective")]
        public DateTime? SIUPLastEff { get; set; }

        [Display(Name = "Initial Asset")]
        public decimal? SIUPInitialAsset { get; set; }

        [Display(Name = "Business Institutional")]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string SIUPBusinessInstitutional { get; set; }

        [Display(Name = "Goods Or Service")]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        public string SIUPGoodsOrService { get; set; }

        [Required]
        [Display(Name = "TDP Number")]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        public string TDPNumber { get; set; }

        [Required]
        [Display(Name = "Begin Effective")]
        public DateTime TDPBeginEff { get; set; }

        [Required]
        [Display(Name = "Last Effective ")]
        public DateTime TDPLastEff { get; set; }

        [Display(Name = "SITU Number")]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        public string SITUNumber { get; set; }

        [Display(Name = "Begin Effective")]
        public DateTime? SITUBeginEff { get; set; }

        [Display(Name = "Last Effective")]
        public DateTime? SITULastEff { get; set; }

        [Display(Name = "Business Area")]
        public long? SITUBusinessArea { get; set; }

        [Display(Name = "Attachment")]
        public int AttachmentID { get; set; }

        public string AttachmentFile { get; set; }

        #endregion

        public List<ListJobTitleViewModel> JobTitleList { get; set; }
        public List<ListLocationDetailViewModel> ListLocation { get; set; }
    }
}
