using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.Location
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        [Remote(action: "ValidateLocationCode", controller: "Location", areaName: "Organization", AdditionalFields = "ID", ErrorMessage = "Location Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character")]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        public int CityId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Postalcode { get; set; }

        [Required]
        public DateTime BeginEff { get; set; }

        public DateTime LastEff { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int ClassificationId { get; set; }



    }
}
