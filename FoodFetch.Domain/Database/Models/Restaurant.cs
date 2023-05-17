using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodFetch.Domain.Database.Models
{
    [Table("tbl_restaurants")]
    public class Restaurant
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("title")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}