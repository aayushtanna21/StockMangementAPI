﻿namespace StockMangementAPI.Models
{
	#region BillDetails Model
	public class BillDetailsModel
    {
        public int BillDetailID { get; set; } // Unique identifier for bill detail
        public int BillID { get; set; } // Link to Bills table
        public DateTime? BillDate { get; set; }
        public int ProductID { get; set; } // Link to Products table
        public string? ProductName { get; set; }
        public int Quantity { get; set; } // Quantity sold
        public decimal UnitPrice { get; set; } // Price per unit (NotNull)
        public decimal SubTotal { get; set; } // Quantity × UnitPrice
        public DateTime? Created { get; set; } // Creation timestamp
        public DateTime? Modified { get; set; } // Last modification timestamp (nullable)
    }
	#endregion
}
