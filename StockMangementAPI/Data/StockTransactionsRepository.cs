using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using StockMangementAPI.Models;
using System.Data;
namespace StockMangementAPI.Data
{
    public class StockTransactionsRepository
    {
        private readonly string _connectionstring;
        public StockTransactionsRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<StockTransactionsModel> SelectAll()
        {
            var stock = new List<StockTransactionsModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_StockTransaction_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stock.Add(new StockTransactionsModel
                    {
                        StockTransactionID = Convert.ToInt32(reader["StockTransactionID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        StockTransactionType = reader["StockTransactionType"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        StockTransactionDate = Convert.ToDateTime(reader["StockTransactionDate"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    });
                }
            }
            return stock;

        }
        #endregion
        #region SelectByID
        public StockTransactionsModel SelectByID(int supplierID)
        {
            StockTransactionsModel stock = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_StockTransaction_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SupplierID", supplierID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stock = new StockTransactionsModel
                    {
                        StockTransactionID = Convert.ToInt32(reader["StockTransactionID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        StockTransactionType = reader["StockTransactionType"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        StockTransactionDate = Convert.ToDateTime(reader["StockTransactionDate"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return stock;
        }
        #endregion
        #region Insert
        public bool Insert(StockTransactionsModel stocktransactionsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_StockTransaction_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", stocktransactionsModel.ProductID);
                cmd.Parameters.AddWithValue("@StockTransactionType", stocktransactionsModel.StockTransactionType);
                cmd.Parameters.AddWithValue("@Quantity", stocktransactionsModel.Quantity);
                cmd.Parameters.AddWithValue("@StockTransactionDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", stocktransactionsModel.UserID);
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(StockTransactionsModel stocktransactionsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_StockTransaction_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@StockTransactionID", stocktransactionsModel.StockTransactionID);
                cmd.Parameters.AddWithValue("@ProductID", stocktransactionsModel.ProductID);
                cmd.Parameters.AddWithValue("@StockTransactionType", stocktransactionsModel.StockTransactionType);
                cmd.Parameters.AddWithValue("@Quantity", stocktransactionsModel.Quantity);
                cmd.Parameters.AddWithValue("@UserID", stocktransactionsModel.UserID);
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int stocktransactionID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_StockTransaction_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("StockTransactionID", stocktransactionID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
        #region UserDropDown
        public List<UserDropDownModel> UserDropDown()
        {
            var user = new List<UserDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_User_DropDown", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.Add(new UserDropDownModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString()
                    });
                }
                return user;
            }
           
        }
		#endregion
		#region ProductDropDown
		public List<ProductDropDownModel> ProductDropDown() {
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
