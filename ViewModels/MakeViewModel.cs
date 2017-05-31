using System.Collections.Generic;

namespace Vega.ViewModels
{
    public class MakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelViewModel> Models {get; set;}
    }
}