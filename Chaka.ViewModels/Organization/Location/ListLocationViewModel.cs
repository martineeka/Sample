using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.Location
{
    public class ListLocationViewModel
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Postalcode { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public bool IsActive { get; set; }
        public int ClassificationId { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public string Classification { get; set; }

    }
}
