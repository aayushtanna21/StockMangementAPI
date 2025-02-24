﻿namespace StockMangementAPI.Models
{
	#region Payment Model
	public class PaymentsModel
    {
        public int PaymentID { get; set; } // Unique payment ID
        public int BillID { get; set; } // Link to Bills table
        public DateTime? BillDate { get; set; }
        public string PaymentMode { get; set; } // Mode of payment (e.g., Cash, Card)
        public decimal AmountPaid { get; set; } // Amount paid
        public DateTime? PaymentDate { get; set; } // Date of payment
        public DateTime? Modified { get; set; } // Last modification timestamp (nullable)
    }
	#endregion
}
