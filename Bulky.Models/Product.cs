using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string ISBN { get; set; }
		[Required] 
		public string AuthurName { get; set; }
		[Required]
		[Range(0, 1000)]
		[Display(Name ="List Price")]
		public float ListPrice { get; set; }
		[Required]
		[Range(0, 1000)]
		[Display(Name = "Price 1-50")]
		public float Price { get; set; }
		[Required]
		[Range(0, 1000)]
		[Display(Name = "Price 50+")]
		public float Price50 { get; set; }
		[Required]
		[Range(0, 1000)]
		[Display(Name = "Price 100+")]
		public float Price100 { get; set; }

        public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }
		[ValidateNever]
		public string ImageUrl { get; set; }
    }
}
