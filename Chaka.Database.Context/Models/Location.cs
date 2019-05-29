using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Location
    {
        public Location()
        {
            EmployeeListFilterLocation = new HashSet<EmployeeListFilterLocation>();
            OrganizationListLocation = new HashSet<OrganizationListLocation>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Postalcode { get; set; }
        public bool IsActive { get; set; }
        public int ClassificationId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }

        public virtual CityDeprecated City { get; set; }
        public virtual LocationClassification Classification { get; set; }
        public virtual CountryDeprecated Country { get; set; }
        public virtual ICollection<EmployeeListFilterLocation> EmployeeListFilterLocation { get; set; }
        public virtual ICollection<OrganizationListLocation> OrganizationListLocation { get; set; }
    }
}
