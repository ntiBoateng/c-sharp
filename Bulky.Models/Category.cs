﻿using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="Length of name cannot exceed 100!")]
        public string Name { get; set; }
        [Range(0, 100, ErrorMessage ="Display Order must be a value between 0 and 100")]
        public int DisplayOrder { get; set; }
    }
}
 