using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NG_Core_Auth.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Discription { get; set; }
        [Required]       
        public string OutOfStock { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]        
        public string Price { get; set; }


    }
}
