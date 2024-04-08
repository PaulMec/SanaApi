using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class OrderProduct
    {
        [Key]
        public int IdOrder { get; set; }
        [Key]
        public int IdProduct { get; set; }
        public int Quantity { get; set; }

        // Relaciones de navegación
        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
