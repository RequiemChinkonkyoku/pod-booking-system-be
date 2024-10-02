using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PodTypeDto
    {
        [Required(ErrorMessage = "PodType name is required")]
        [StringLength(100, ErrorMessage = "PodType name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Price { get; set; }

    }
}
