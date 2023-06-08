using Projekt.Data.Data.Sharded;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Data.Data.Shop
{
    public class CheckoutItem: BaseData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string SessionId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
