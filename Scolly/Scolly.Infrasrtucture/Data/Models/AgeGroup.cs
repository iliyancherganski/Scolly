﻿using System.ComponentModel.DataAnnotations;

namespace Scolly.Infrastructures.Data.Models
{
    public class AgeGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
