using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.Admin
{
    public class SuppliersRepository
    {
        private readonly string _connectionstring;
        public SuppliersRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<SuppliersModel> SelectAll()
        {
            var suppliers = new List<SuppliersModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Supplier_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    suppliers.Add(new SuppliersModel
                    {
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString(),
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
            return suppliers;
        }
        #endregion
        #region SelectByID
        public SuppliersModel SelectByID(int supplierID)
        {
            SuppliersModel suppliers = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Supplier_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SupplierID", supplierID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    suppliers = new SuppliersModel
                    {
                        SupplierID = Convert.ToInt32(reader["SupplierID"]),
                        SupplierName = reader["SupplierName"].ToString(),
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
            return suppliers;
        }
        #endregion
        #region Insert
        public bool Insert(SuppliersModel suppliersModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Supplier_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@SupplierName", suppliersModel.SupplierName);
                cmd.Parameters.AddWithValue("@PhoneNumber", suppliersModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", suppliersModel.Email);
                cmd.Parameters.AddWithValue("@Address", suppliersModel.Address);
                cmd.Parameters.AddWithValue("@UserID", suppliersModel.UserID);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now); // Ensure @Created is provided
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(SuppliersModel suppliersModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Supplier_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@SupplierID", suppliersModel.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierName", suppliersModel.SupplierName);
                cmd.Parameters.AddWithValue("@PhoneNumber", suppliersModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", suppliersModel.Email);
                cmd.Parameters.AddWithValue("@Address", suppliersModel.Address);
                cmd.Parameters.AddWithValue("@UserID", suppliersModel.UserID);
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int supplierID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Supplier_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("SupplierID", supplierID);
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
            #endregion
        }
    }
}
