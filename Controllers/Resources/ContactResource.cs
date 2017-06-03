using System.ComponentModel.DataAnnotations;

namespace Vega.Controllers.Resources
{
    public class ContactResource
    {

        [Required (ErrorMessage="Contact name is required.")]
        [StringLength(255)]
        public string  Name { get; set; }
        
        [Required (ErrorMessage="Contact email is required.")]
        [StringLength(255)]
        public string  Email { get; set; }


        [Required (ErrorMessage="Contact phone is required.")]
        [StringLength(255)]
        public string  Phone { get; set; }
    }
}