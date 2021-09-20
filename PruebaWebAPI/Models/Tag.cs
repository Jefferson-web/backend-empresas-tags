using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Models
{
    public class Tag
    {
        [Key]
        public int tagId { get; set; }
        public string nombre { get; set; }
    }
}
