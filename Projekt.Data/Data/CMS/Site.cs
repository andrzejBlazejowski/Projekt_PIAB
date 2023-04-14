using Projekt.Data.Data.Sharded;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Data.Data.CMS
{
    public class Site : BaseSeoData
    {
        [Required(ErrorMessage = "Treść musi zostać wypełniona")]
        [MaxLength(600, ErrorMessage = "Treść może zawierać maksymalnie 600 znaków ")]
        [Display(Name = "Treść")]
        public string? Content { get; set; }

        public int HeaderImageId { get; set; }
        public Picture? HeaderImage { get; set; }
    }
}
