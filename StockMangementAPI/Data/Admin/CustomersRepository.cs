using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.Admin
{
    public class CustomersRepository
    {
        private readonly string _connectionstring;
        public CustomersRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<CustomersModel> SelectAll()
        {
            var customers = new List<CustomersModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new CustomersModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
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
        public CustomersModel SelectByID(int CustomerID)
        {
            CustomersModel customers = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers = new CustomersModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return customers;
        }
        #endregion
        #region Insert
        public bool Insert(CustomersModel customersModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@CustomerName", customersModel.CustomerName);
                cmd.Parameters.AddWithValue("@PhoneNumber", customersModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", customersModel.Email);
                cmd.Parameters.AddWithValue("@Address", customersModel.Address);
                cmd.Parameters.AddWithValue("@UserID", customersModel.UserID);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now); // Ensure @Created is provided
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(CustomersModel customersModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@CustomerID", customersModel.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", customersModel.CustomerName);
                cmd.Parameters.AddWithValue("@PhoneNumber", customersModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", customersModel.Email);
                cmd.Parameters.AddWithValue("@Address", customersModel.Address);
                cmd.Parameters.AddWithValue("@UserID", customersModel.UserID);
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("CustomerID", customerID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
    }
}
