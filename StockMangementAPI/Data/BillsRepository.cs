using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data
{
    public class BillsRepository
    {
        private readonly string _connectionstring;
        public BillsRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<BillsModel> SelectAll()
        {
            var bills = new List<BillsModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bills.Add(new BillsModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),                        
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
						UserName = reader["UserName"].ToString(),
						Modified = Convert.ToDateTime(reader["Modified"])
                    });

                }
            }
            return bills;
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
		#region SelectByID
		public BillsModel SelectByID(int billID)
        {
            BillsModel bills = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("@BillID", billID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bills = new BillsModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName= reader["CustomerName"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
						UserName = reader["UserName"].ToString(),
						Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return bills;
        }
        #endregion
        #region Insert
        public bool Insert(BillsModel billsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@CustomerID", billsModel.CustomerID);
                cmd.Parameters.AddWithValue("@TotalAmount", billsModel.TotalAmount);
                cmd.Parameters.AddWithValue("@Discount", billsModel.Discount);
                cmd.Parameters.AddWithValue("@PaymentMode", billsModel.PaymentMode);
                cmd.Parameters.AddWithValue("@BillDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", billsModel.UserID);
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(BillsModel billsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@BillID", billsModel.BillID);
                cmd.Parameters.AddWithValue("@CustomerID", billsModel.CustomerID);
                cmd.Parameters.AddWithValue("@TotalAmount", billsModel.TotalAmount);
                cmd.Parameters.AddWithValue("@Discount", billsModel.Discount);
                cmd.Parameters.AddWithValue("@PaymentMode", billsModel.PaymentMode);
                cmd.Parameters.AddWithValue("@UserID", billsModel.UserID);
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int billID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("@billID", billID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
    }
}
