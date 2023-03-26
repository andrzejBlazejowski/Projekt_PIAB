using Projekt.Data.Data.Shop;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Data.Data.Sharded
{
    public class Image : BaseData
    {
        [Required(ErrorMessage = "musisz wybrać obrazek")]
        [Display(Name = "obrazek")]
        public string ImageData { get; set; }

        public List<Product> Product { get; set; }
    }
}
