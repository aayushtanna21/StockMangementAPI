using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.Admin
{
    public class PaymentsRepository
    {
        private readonly string _connectionstring;
        public PaymentsRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<PaymentsModel> SelectAll()
        {
            var payments = new List<PaymentsModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    payments.Add(new PaymentsModel
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    });

                }
            }
            return payments;
        }
        #endregion
        #region BillsDropDown
        public List<BillsDropDownModel> BillsDropDown()
        {
            var bills = new List<BillsDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_DropDown", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bills.Add(new BillsDropDownModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillInfo = Convert.ToString(reader["BillInfo"])
                    });
                }
            }
            return bills;
        }
        #endregion
        #region SelectByID
        public PaymentsModel SelectByID(int paymentID)
        {
            PaymentsModel payments = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("PaymentID", paymentID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    payments = new PaymentsModel
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return payments;
        }
        #endregion
        #region Insert
        public bool Insert(PaymentsModel paymentsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@BillID", paymentsModel.BillID);
                cmd.Parameters.AddWithValue("@PaymentMode", paymentsModel.PaymentMode);
                cmd.Parameters.AddWithValue("@AmountPaid", paymentsModel.AmountPaid);
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now); // Ensure @Created is provided
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(PaymentsModel paymentsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@PaymentID", paymentsModel.PaymentID);
                cmd.Parameters.AddWithValue("@BillID", paymentsModel.BillID);
                cmd.Parameters.AddWithValue("@PaymentMode", paymentsModel.PaymentMode);
                cmd.Parameters.AddWithValue("@AmountPaid", paymentsModel.AmountPaid);
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int paymentID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("PaymentID", paymentID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
    }
}
