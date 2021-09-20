using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Models.DTOs
{
    public class EmpresaDTO
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string ruc { get; set; }
        public List<TagDTO> tags { get; set; }
    }
}
