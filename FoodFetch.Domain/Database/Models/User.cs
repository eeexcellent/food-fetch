using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using FoodFetch.Contracts.Enums;

using Microsoft.EntityFrameworkCore;

namespace FoodFetch.Domain.Database.Models
{
    [Table("tbl_users")]
    [Index(nameof(Email), IsUnique = true)]
    public class DatabaseUser
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("first_name")]
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Column("second_name")]
        [MaxLength(100)]
        public string SecondName { get; set; }
        [Column("role")]
        public Role Role { get; set; }
        [Column("email")]
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<DatabaseOrder> Orders { get; set; }
    }
}