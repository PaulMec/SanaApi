namespace SanaStoreAPI.Domain.Models
{
    /// <summary>
    /// Represents an order in the store.
    /// </summary>
    public class OrdersDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the order.
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the customer who placed the order.
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the list of order details for this order.
        /// </summary>
        public List<OrderDetailDTO> OrderDetails { get; set; }

        /// <summary>
        /// Navigation property to the customer who placed the order.
        /// </summary>
        public CustomersDTO Customer { get; set; }
    }
}
