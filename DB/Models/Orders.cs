using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        // Corrección: Agregar JsonIgnore para evitar serialización cíclica
        [JsonIgnore]
        [ForeignKey("CustomerID")]
        public virtual Customers Customer { get; set; }

        // Corrección: Agregar JsonIgnore para evitar serialización cíclica
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
