using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class EmployeeBiodata
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TitleId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthCityId { get; set; }
        public int BirthStateId { get; set; }
        public int BirthCountryId { get; set; }
        public string PersonalEmailAddress { get; set; }
        public string WorkEmailAddress { get; set; }
        public string IdentityNumber { get; set; }
        public int IdentityTypeId { get; set; }
        public int MaritalStatusId { get; set; }
        public string FamilyCardNumber { get; set; }
        public string StatusInFamilyCard { get; set; }
        public int NationalityId { get; set; }
        public bool IsCitizen { get; set; }
        public int? EthnicOriginId { get; set; }
        public int? ReligionId { get; set; }
        public int? BloodTypeId { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? UniformSizeId { get; set; }
        public string PhysicalCondition { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
    }
}
