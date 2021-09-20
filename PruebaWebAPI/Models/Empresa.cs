using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Models
{
    public class Empresa
    {
        [Key]
        public int empresaId { get; set; }
        public string nombre { get; set; }
        public string ruc { get; set; }
        public DateTime fecha_registro { get; set; }
        public List<Tag> tags { get; set; } = new List<Tag>();
    }
}
