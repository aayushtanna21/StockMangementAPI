using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.User
{
    public class UserSalesRepository
    {
        private readonly string _connectionstring;
        public UserSalesRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<UserSalesModel> SelectAll()
        {
            var sales = new List<UserSalesModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Sales_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sales.Add(new UserSalesModel
                    {
                        SalesID = Convert.ToInt32(reader["SalesID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                        AmountDue = Convert.ToDecimal(reader["AmountDue"]),
                        SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    });
                }
            }
            return sales;

        }
        #endregion
        #region SelectByID
        public UserSalesModel SelectByID(int salesID)
        {
            UserSalesModel sales = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Sales_SelectByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SalesID", salesID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sales = new UserSalesModel
                    {
                        SalesID = Convert.ToInt32(reader["SalesID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                        AmountDue = Convert.ToDecimal(reader["AmountDue"]),
                        SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    };
                }
            }
            return sales;
        }
        #endregion
        #region Insert
        public bool Insert(UserSalesModel usersalesModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Sales_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", usersalesModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", usersalesModel.Quantity);
                cmd.Parameters.AddWithValue("@Price", usersalesModel.Price);
                cmd.Parameters.AddWithValue("@AmountPaid", usersalesModel.AmountPaid);
                cmd.Parameters.AddWithValue("@AmountDue", usersalesModel.AmountDue);
                cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerID", usersalesModel.CustomerID);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(UserSalesModel usersalesModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Sales_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@SalesID", usersalesModel.SalesID);
                cmd.Parameters.AddWithValue("@ProductID", usersalesModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", usersalesModel.Quantity);
                cmd.Parameters.AddWithValue("@Price", usersalesModel.Price);
                cmd.Parameters.AddWithValue("@AmountPaid", usersalesModel.AmountPaid);
                cmd.Parameters.AddWithValue("@AmountDue", usersalesModel.AmountDue);
                cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerID", usersalesModel.CustomerID);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int salesID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Sales_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SalesID", salesID);
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
