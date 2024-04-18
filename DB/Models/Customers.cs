using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    /// <summary>
    /// Represents a customer in the database.
    /// </summary>
    public class Customers
    {
        /// <summary>
        /// Gets or sets the primary key for the customer.
        /// </summary>
        /// <value>
        /// The unique identifier for the customer.
        /// </value>
        [Key]
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        /// <value>
        /// The first name of the customer. This field is required and has a maximum length of 50 characters.
        /// </value>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        /// <value>
        /// The last name of the customer. This field is required and has a maximum length of 50 characters.
        /// </value>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        /// <value>
        /// The email address of the customer. This field is required and has a maximum length of 100 characters.
        /// </value>
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Navigation property for accessing the orders placed by the customer.
        /// </summary>
        /// <value>
        /// A collection of orders associated with the customer.
        /// </value>
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
