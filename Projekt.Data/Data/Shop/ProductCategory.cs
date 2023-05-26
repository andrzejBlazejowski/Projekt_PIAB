using Projekt.Data.Data.Sharded;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Data.Data.Shop
{
    public class ProductCategory : BaseSeoData
    {
        [Required(ErrorMessage = "tytuł linku musi zostać wypełniona")]
        [MaxLength(600, ErrorMessage = "tytuł linku może zawierać maksymalnie 600 znaków ")]
        [Display(Name = "tytuł linku")]
        public string? LinkTitle { get; set; }

        public List<Product>? Product { get; set; }

    }
}
