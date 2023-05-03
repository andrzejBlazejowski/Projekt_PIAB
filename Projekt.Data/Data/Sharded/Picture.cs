using Projekt.Data.Data.CMS;
using Projekt.Data.Data.Shop;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Data.Data.Sharded
{
    public class Picture : BaseData
    {
        [Required(ErrorMessage = "musisz podać link do obrazeka")]
        [Display(Name = "obrazek")]
        public string ImageData { get; set; }

        public List<Product>? Product { get; set; }
        public List<Site>? Site { get; set; }
        public List<BlogPost>? BlogPost { get; set; }
    }
}
