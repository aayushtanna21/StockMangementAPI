using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.User
{
    public class UserSalesReturnRepository
    {
        private readonly string _connectionstring;
        public UserSalesReturnRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<UserSalesReturnModel> SelectAll()
        {
            var salesreturn = new List<UserSalesReturnModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_SalesReturn_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    salesreturn.Add(new UserSalesReturnModel
                    {
                        SalesReturnID = Convert.ToInt32(reader["SalesReturnID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        SaleReturnDate = Convert.ToDateTime(reader["SaleReturnDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    });
                }
            }
            return salesreturn;

        }
        #endregion
        #region SelectByID
        public UserSalesReturnModel SelectByID(int salesreturnID)
        {
            UserSalesReturnModel salesreturn = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_SalesReturn_SelectByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SalesReturnID", salesreturnID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    salesreturn = new UserSalesReturnModel
                    {
                        SalesReturnID = Convert.ToInt32(reader["SalesID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        SaleReturnDate = Convert.ToDateTime(reader["SaleReturnDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    };
                }
            }
            return salesreturn;
        }
        #endregion
        #region Insert
        public bool Insert(UserSalesReturnModel usersalesreturnModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_SalesReturn_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", usersalesreturnModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", usersalesreturnModel.Quantity);
                cmd.Parameters.AddWithValue("@SalesDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerID", usersalesreturnModel.CustomerID);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(UserSalesReturnModel usersalesreturnModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_SalesReturn_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", usersalesreturnModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", usersalesreturnModel.Quantity);
                cmd.Parameters.AddWithValue("@SalesDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerID", usersalesreturnModel.CustomerID);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int salesreturnID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_SalesReturn_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SalesReturnID", salesreturnID);
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
    }
}
