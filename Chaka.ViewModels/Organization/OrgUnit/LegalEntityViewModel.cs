using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
  public class LegalEntityViewModel
    {
            #region org unit

            public string ID { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }

            [Display(Name = "Type")]
            public string OrganizationUnitTypeName { get; set; }

            //[Display(Name = "Location")]
            //public string LocationName { get; set; }


            [Display(Name = "Structural Superior")]
            public string StructuralSuperiorName { get; set; }

            [Display(Name = "Cost Center")]
            public string CostCenterName { get; set; }

            public string Description { get; set; }

            [Display(Name = "Begin Effective Date")]
            public DateTime BeginEff { get; set; }

            [Display(Name = "Last Effective Date")]
            public DateTime? LastEff { get; set; }

            [Display(Name = "Category")]
            public string OrganizationUnitCategoryName { get; set; }

            [Display(Name = "Parent Unit")]
            public string OrgUnit1Name { get; set; }

            public int? LegalEntityInformationID { get; set; }

            #endregion

            #region legal entity

            [Display(Name = "Currency")]
            public string CurrencyName { get; set; }

            [Display(Name = "Managing Director")]
            public string ManagingDirectorName { get; set; }

            [Display(Name = "Phone Number 1")]
            public string PhoneNumber1 { get; set; }

            [Display(Name = "Phone Number 2")]
            public string PhoneNumber2 { get; set; }

            [Display(Name = "Fax Number 1")]
            public string FaxNumber1 { get; set; }

            [Display(Name = "Fax Number 2")]
            public string FaxNumber2 { get; set; }

            public string Address { get; set; }

            [Display(Name = "Tax File Number")]
            public string TaxFileNumber { get; set; }

            [Display(Name = "Register Date")]
            public DateTime TFNRegisterDate { get; set; }

            [Display(Name = "KPP Branch")]
            public string KPPBranchName { get; set; }

            public string SPPKPNumber { get; set; }

            [Display(Name = "Release Date")]
            public DateTime SPPKPReleaseDate { get; set; }

            [Display(Name = "Business Field Regulation")]
            public string BusinessFieldRegulationName { get; set; }

            [Display(Name = "Business Field Category")]
            public string BusinessFieldCategoryName { get; set; }

            [Display(Name = "Business Field Classification")]
            public string BusinessFieldClassificationMainSectionName { get; set; }

            [Display(Name = "Company Brand")]
            public string CompanyBrandName { get; set; }

            [Display(Name = "Funding")]
            public string FundingName { get; set; }

            [Display(Name = "Business Status")]
            public string BusinessStatusOwnershipName { get; set; }

            [Display(Name = "Tax Duty")]
            public string TaxDutyName { get; set; }

            [Display(Name = "Tax Invoice Serial Code")]
            public string TaxInvoiceSerialCode { get; set; }

            [Display(Name = "SIUP Number")]
            public string SIUPNumber { get; set; }

            [Display(Name = "Class")]
            public string SIUPClassName { get; set; }

            [Display(Name = "Begin Effective Date")]
            public DateTime? SIUPBeginEff { get; set; }

            [Display(Name = "Last Effective Date")]
            public DateTime? SIUPLastEff { get; set; }

            [Display(Name = "Initial Asset")]
            public decimal? SIUPInitialAsset { get; set; }

            [Display(Name = "Business Institutional")]
            public string SIUPBusinessInstitutional { get; set; }

            [Display(Name = "Goods Or Service")]
            public string SIUPGoodsOrService { get; set; }

            [Display(Name = "TDP Number")]
            public string TDPNumber { get; set; }

            [Display(Name = "Begin Effective Date")]
            public DateTime TDPBeginEff { get; set; }

            [Display(Name = "Last Effective Date")]
            public DateTime TDPLastEff { get; set; }

            [Display(Name = "SITU Number")]
            public string SITUNumber { get; set; }

            [Display(Name = "Begin Effective Date")]
            public DateTime? SITUBeginEff { get; set; }

            [Display(Name = "Last Effective Date")]
            public DateTime? SITULastEff { get; set; }

            [Display(Name = "Business Area")]
            public long? SITUBusinessArea { get; set; }

            [Display(Name = "Attachment")]
            public string AttachmentAttachmentFile { get; set; }

            #endregion
        }
    }


