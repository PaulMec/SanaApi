namespace SanaStoreAPI.Domain.Models
{
    /// <summary>
    /// Represents a detail of an order in the store.
    /// </summary>
    public class OrderDetailDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the order detail.
        /// </summary>
        public int OrderDetailID { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product included in the order detail.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product included in the order detail.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the product at the time the order was placed.
        /// </summary>
        public decimal Price { get; set; }
    }
}
