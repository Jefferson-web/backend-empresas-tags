using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Models
{
    public class EmpresaTag
    {
        [Key]
        public int id { get; set; }
        public int empresaId { get; set; }
        public int tagId { get; set; }
    }
}
