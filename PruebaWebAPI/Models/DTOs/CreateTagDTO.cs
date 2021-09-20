using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Models.DTOs
{
    public class CreateTagDTO
    {
        [Required]
        public string nombre { get; set; }
    }
}
