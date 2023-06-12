using Projekt.Data.Data.Sharded;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Data.Data.Shop
{
    public class Product : BaseSeoData
    {
        [Required(ErrorMessage = "Treść musi zostać wypełniona")]
        [MaxLength(600, ErrorMessage = "Treść może zawierać maksymalnie 600 znaków ")]
        [Display(Name = "Treść")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Pole Cena jest aryckluczowa w produktach, dlatego uprzejmie uprasza się o uzupełnienie jej")]
        [Column(TypeName = "decimal")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal")]
        [Display(Name = "Stawka Vat")]
        public decimal VatRate { get; set; }

        [Required(ErrorMessage = "Pole Ilość w magazynie jest wymagana w produktach")]
        [Column(TypeName = "int")]
        [Display(Name = "Ilośc w magazynie")]
        public int CountInWearhouse { get; set; }

        [Required(ErrorMessage = "Pole Czy widoczny musi zostać wypełnione")]
        [Column(TypeName = "bit")]
        [Display(Name = "Czy widoczny")]
        public bool IsVisible { get; set; }

        [MaxLength(50, ErrorMessage = "Marka może zawierać maksymalnie 50 znaków ")]
        [Display(Name = "Marka")]
        public string? BrandName { get; set; }

        public int ImageId { get; set; }
        public Picture? Image { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public List<CheckoutItem>? CheckoutItem { get; set; }
    }
}
