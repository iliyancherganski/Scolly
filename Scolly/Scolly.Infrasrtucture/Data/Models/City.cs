﻿using System.ComponentModel.DataAnnotations;

namespace Scolly.Infrastructure.Data.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
