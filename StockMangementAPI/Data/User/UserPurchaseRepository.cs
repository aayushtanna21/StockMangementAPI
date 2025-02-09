using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.User
{
    public class UserPurchaseRepository
    {
        private readonly string _connectionstring;
        public UserPurchaseRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<UserPurchaseModel> SelectAll()
        {
            var purchase = new List<UserPurchaseModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Purchase_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    purchase.Add(new UserPurchaseModel
                    {
                        PurchaseID = Convert.ToInt32(reader["PurchaseID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        CostPrice = Convert.ToInt32(reader["CostPrice"]),
                        PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"])
                    });
                }
            }
            return purchase;

        }
        #endregion
        #region SelectByID
        public UserPurchaseModel SelectByID(int purchaseID)
        {
            UserPurchaseModel purchase = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Purchase_SelectByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("PurchaseID", purchaseID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    purchase = new UserPurchaseModel
                    {
                        PurchaseID = Convert.ToInt32(reader["PurchaseID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        CostPrice = Convert.ToInt32(reader["CostPrice"]),
                        PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"])
                    };
                }
            }
            return purchase;
        }
        #endregion
        #region Insert
        public bool Insert(UserPurchaseModel userpurchaseModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Purchase_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", userpurchaseModel.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", userpurchaseModel.CustomerID);
                cmd.Parameters.AddWithValue("@SupplierID", userpurchaseModel.SupplierID);
                cmd.Parameters.AddWithValue("@Quantity", userpurchaseModel.Quantity);
                cmd.Parameters.AddWithValue("@CostPrice", userpurchaseModel.CostPrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(UserPurchaseModel userpurchaseModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Purchase_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@PurchaseID", userpurchaseModel.PurchaseID);
                cmd.Parameters.AddWithValue("@ProductID", userpurchaseModel.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", userpurchaseModel.CustomerID);
                cmd.Parameters.AddWithValue("@SupplierID", userpurchaseModel.SupplierID);
                cmd.Parameters.AddWithValue("@Quantity", userpurchaseModel.Quantity);
                cmd.Parameters.AddWithValue("@CostPrice", userpurchaseModel.CostPrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int purchaseID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Purchase_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("PurchaseID", purchaseID);
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
