using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Field name is required.")]
        [StringLength(50, ErrorMessage="Field name max length of 50 caracters.")]
        public string name { get; set; }

        [Required(ErrorMessage="Field make is required.")]
        public int MakeId { get; set; }
    }
}