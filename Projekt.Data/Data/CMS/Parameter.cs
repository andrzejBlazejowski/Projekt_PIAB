using Projekt.Data.Data.Sharded;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Data.Data.CMS
{
    public class Parameter : BaseData
    {
        [Required(ErrorMessage = "Klucz musi zostać wypełniona")]
        [MaxLength(50, ErrorMessage = "Klucz może zawierać maksymalnie 50 znaków ")]
        [Display(Name = "Klucz")]
        public string? Key { get; set; }

        [Required(ErrorMessage = "Wartość musi zostać wypełniona")]
        [MaxLength(500, ErrorMessage = "Wartość może zawierać maksymalnie 500 znaków ")]
        [Display(Name = "Wartość")]
        public string? Value { get; set; }
    }
}
