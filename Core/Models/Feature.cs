using System.ComponentModel.DataAnnotations;

namespace Vega.Core.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Field name is required.")]
        [StringLength(50, ErrorMessage="Field name max length of 50 caracters.")]
        public string name { get; set; }
    }
}