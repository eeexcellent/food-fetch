using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodFetch.Domain.Database.Models
{
    [Table("tbl_orders_products")]
    public class OrderProduct
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public DatabaseOrder Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        public DatabaseProduct Product { get; set; }
    }
}