using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class ProductCategory
    {
        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products Product { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}
