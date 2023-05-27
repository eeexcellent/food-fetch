using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using FoodFetch.Contracts.Enums;

namespace FoodFetch.Domain.Database.Models
{
    [Table("tbl_orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("status")]
        public OrderStatus Status { get; set; }
        [Column("ordered_at")]
        public DateTime OrderedAt { get; set; }
        [Column("closed_at")]
        public DateTime? ClosedAt { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual DatabaseUser User { get; set; }

        public virtual ICollection<DatabaseProduct> Products { get; set; }
    }
}