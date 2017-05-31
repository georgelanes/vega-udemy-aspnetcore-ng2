using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vega.Models
{
    public class Make
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Field name is required")]
        [StringLength(50, ErrorMessage="Field name max length of 50 caracters.")]
        public string name { get; set; }

        public virtual ICollection<Model> Models {get; set;}
    }
}