namespace SanaStoreAPI.Domain.Models
{
     /// <summary>
     /// Represents a customer in the store.
     /// </summary>
    public class CustomersDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string Email { get; set; }

    }
}
