namespace StockMangementAPI.Models
{
	#region Product Model
	public class ProductModel
    {
        public int ProductID { get; set; } // Unique identifier for products
        public string ProductName { get; set; } // Name of the product
		public string? ProductImage { get; set; }
		public int CategoryID { get; set; } // Link to Categories table
        public string? CategoryName { get; set; }
        public int StockQuantity { get; set; } // Current stock level
        public decimal Price { get; set; } // Selling price of the product
        public decimal CostPrice { get; set; } // Purchase price
        public int ReorderLevel { get; set; } // Minimum stock level for reorder
        public string Unit { get; set; } // Unit of measurement (e.g., kg)
        public int CustomerID { get; set; } // User who performed the transaction
        public string? CustomerName { get; set; }    
        public DateTime? Created { get; set; } // Creation timestamp
        public DateTime? Modified { get; set; }
    }
	#endregion
	#region Product DropDownModel
	public class ProductDropDownModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
	#endregion
}
