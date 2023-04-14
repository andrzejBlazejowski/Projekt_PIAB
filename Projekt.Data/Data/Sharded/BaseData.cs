using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Data.Data.Sharded
{
    public class BaseData
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Nazwa może zawierać maksymalnie 50 znaków ")]
        [Required(ErrorMessage = "Pole Nazwa musi być wypełnione")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Opis może zawierać maksymalnie 250 znaków ")]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Pole Czy aktywny musi być zdefiniowany - to najprawdopodobniej problem z programem")]
        [Column(TypeName = "bit")]
        [Display(Name = "Czy aktywny")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Pole LastModificationDate musi być zdefiniowany - to najprawdopodobniej problem z programem")]
        [Column(TypeName = "datetime")]
        [Display(Name = "ostatnio zmodyfikowany")]
        public DateTime LastModificationDate { get; set; }

        [Required(ErrorMessage = "Pole LastModifiedBy musi być zdefiniowany - to najprawdopodobniej problem z programem")]
        [Display(Name = "Ostatnio zmodyfikowany przez")]
        public int LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Pole CreationDate musi być zdefiniowany - to najprawdopodobniej problem z programem")]
        [Column(TypeName = "datetime")]
        [Display(Name = "Stworzony")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Pole CreatedBy musi być zdefiniowany - to najprawdopodobniej problem z programem")]
        [Display(Name = "Stworzony przez")]
        public int CreatedBy { get; set; }
    }
}
