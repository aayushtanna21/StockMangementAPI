﻿namespace StockMangementAPI.Models
{
	#region Bills Model
	public class BillsModel
    {
        public int BillID { get; set; } // Unique bill identifier
        public int CustomerID { get; set; } // Link to Customers table
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; } // Total bill amount
        public decimal? Discount { get; set; } // Discount applied (nullable)
        public string PaymentMode { get; set; } // Mode of payment (e.g., Cash, Card)
        public DateTime? BillDate { get; set; } // Date of the bill
        public int UserID { get; set; } // Link to the user who created the bill
        public string? UserName { get; set; }
        public DateTime? Modified { get; set; } // Last modification timestamp (nullable)
    }
	#endregion
	#region Bills DropDownModel
	public class BillsDropDownModel
    {
        public int BillID { get; set; }
        public string BillInfo { get; set; }
    }
	#endregion
}
