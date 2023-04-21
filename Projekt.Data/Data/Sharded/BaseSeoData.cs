using System.ComponentModel.DataAnnotations;

namespace Projekt.Data.Data.Sharded
{
    public class BaseSeoData : BaseData
    {
        [MaxLength(100, ErrorMessage = "Opis może zawierać maksymalnie 100 znaków ")]
        [Display(Name = "Meta tytuł")]
        public string? MetaTitle { get; set; }

        [MaxLength(250, ErrorMessage = "Opis może zawierać maksymalnie 250 znaków ")]
        [Display(Name = "Meta opis")]
        public string? MetaDescription { get; set; }
/*
        [MaxLength(250, ErrorMessage = "przyjazny link dla strony")]
        [Display(Name = "link")]
        public string? FriendlyLink { get; set; }*/
    }
}
