using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ss.RealEstate.Web.Models
{
    public class HomeViewModel
    {
        public PropertyFormRequestViewModel PropertyFormVM { get; set; }
        public PropertyCollectionViewModel PropertyCollectionVM { get; set; }
        public PropertyCollectionViewModel PropertyCollection2VM { get; set; }
    }

    public class PropertyFormRequestViewModel //: IValidatableObject
    {
        [Required]
        public string City { get; set; }

        [Required]
        public uint MinYear { get; set; }

        [Required]
        public uint MaxYear { get; set; }

        [Required]
        public uint MinPrice { get; set; }

        [Required]
        public uint MaxPrice { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class PropertyCollectionViewModel
    {

    }
}