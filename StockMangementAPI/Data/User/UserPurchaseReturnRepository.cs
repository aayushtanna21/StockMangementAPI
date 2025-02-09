using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.User
{
    public class UserPurchaseReturnRepository
    {
        private readonly string _connectionstring;
        public UserPurchaseReturnRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<UserPurchaseReturnModel> SelectAll()
        {
            var purchasereturn = new List<UserPurchaseReturnModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_PurchaseReturn_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    purchasereturn.Add(new UserPurchaseReturnModel
                    {
                        PurchaseReturnID = Convert.ToInt32(reader["PurchaseReturnID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        PurchaseReturnDate = Convert.ToDateTime(reader["PurchaseReturnDate"])
                    });
                }
            }
            return purchasereturn;

        }
        #endregion
        #region SelectByID
        public UserPurchaseReturnModel SelectByID(int purchasereturnID)
        {
            UserPurchaseReturnModel purchase = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_PurchaseReturn_SelectByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("PurchaseReturnID", purchasereturnID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    purchase = new UserPurchaseReturnModel
                    {
                        PurchaseReturnID = Convert.ToInt32(reader["PurchaseReturnID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        PurchaseReturnDate = Convert.ToDateTime(reader["PurchaseReturnDate"])
                    };
                }
            }
            return purchase;
        }
        #endregion
        #region Insert
        public bool Insert(UserPurchaseReturnModel userpurchasereturnModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_PurchaseReturn_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", userpurchasereturnModel.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", userpurchasereturnModel.CustomerID);
                cmd.Parameters.AddWithValue("@SupplierID", userpurchasereturnModel.SupplierID);
                cmd.Parameters.AddWithValue("@Quantity", userpurchasereturnModel.Quantity);
                cmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(UserPurchaseReturnModel userpurchasereturnModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_PurchaseReturn_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@PurchaseReturnID", userpurchasereturnModel.PurchaseReturnID);
                cmd.Parameters.AddWithValue("@ProductID", userpurchasereturnModel.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", userpurchasereturnModel.CustomerID);
                cmd.Parameters.AddWithValue("@SupplierID", userpurchasereturnModel.SupplierID);
                cmd.Parameters.AddWithValue("@Quantity", userpurchasereturnModel.Quantity);
                cmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int purchasereturnID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_PurchaseReturn_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("PurchaseReturnID", purchasereturnID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
        #region CustomerDropDown
        public List<CustomersDropDownModel> CustomerDropDown()
        {
            var customers = new List<CustomersDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_DropDown", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new CustomersDropDownModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    });
                }
            }
            return customers;
        }
        #endregion
        #region ProductDropDown
        public List<ProductDropDownModel> ProductDropDown()
        {
            var products = new List<ProductDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_DropDown", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new ProductDropDownModel()
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString()
                    });
                }
                return products;
            }
        }
        #endregion
        #region SuppliersDropDown
        public List<SuppliersDropDownModel> SuppliersDropDown()
        {
            var suppliers = new List<SuppliersDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Supplier_DropDown", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    suppliers.Add(new SuppliersDropDownModel()
                    {
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString()
                    });
                }
                return suppliers;
            }
        }
        #endregion
    }
}
