using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class PodDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name can have a maximum of 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description can have a maximum of 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Range(0, 1, ErrorMessage = "Status must be either 0 (inactive) or 1 (active).")]
        public int Status { get; set; }

        [Required(ErrorMessage = "PodTypeId is required.")]
        public int PodTypeId { get; set; }

        [Required(ErrorMessage = "AreaId is required.")]
        public int AreaId { get; set; }

    }
}
