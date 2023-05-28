using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using FoodFetch.Contracts.Enums;

namespace FoodFetch.Domain.Database.Models
{
    [Table("tbl_orders")]
    public class DatabaseOrder
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
        [Column("request")]
        public string Request { get; set; }
        [Column("delivery_place")]
        [Required]
        public string DeliveryPlace { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual DatabaseUser User { get; set; }

        public virtual ICollection<DatabaseProduct> Products { get; set; }
    }
}