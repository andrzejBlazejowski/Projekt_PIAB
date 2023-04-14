using Projekt.Data.Data.Sharded;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Data.Data.CMS
{
    public class BlogPost: BaseSeoData
    {
        [Required(ErrorMessage = "Treść musi zostać wypełniona")]
        [MaxLength(600, ErrorMessage = "Treść może zawierać maksymalnie 600 znaków ")]
        [Display(Name = "Treść")]
        public string? Content { get; set; }

        public int HeaderImageId { get; set; }
        [Display(Name = "Obrazek Nagłówka")]
        public Picture? HeaderImage { get; set; }

    }
}
