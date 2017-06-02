using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vega.Models;

namespace Vega.ViewModels
{

    public class VehicleViewModel
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }

        [Required(ErrorMessage="Contact is required")]
        public ContactViewModel Contact { get; set; }

        public ICollection<int> Features { get; set; }

       public VehicleViewModel()
       {
           Features = new Collection<int>();
       }
    }
}