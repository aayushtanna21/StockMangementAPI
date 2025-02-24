﻿namespace StockMangementAPI.Models
{
	#region Suppliers Model
	public class SuppliersModel
    {
        public int SupplierID { get; set; } // Unique identifier for suppliers
        public string SupplierName { get; set; } // Name of the supplier
        public string PhoneNumber { get; set; } // Contact number
        public string Email { get; set; } // Email address (NotNull)
        public string Address { get; set; } // Address of the supplier
        public int UserID { get; set; } // User who performed the transaction
        public string? UserName {  get; set; }
        public DateTime? Created { get; set; } // Creation timestamp
        public DateTime? Modified { get; set; } // Last modification timestamp (nullable)
    }
    #endregion
    public class SuppliersDropDownModel
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}
