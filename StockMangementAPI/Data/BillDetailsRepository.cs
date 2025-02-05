using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data
{
    public class BillDetailsRepository
    {
        private readonly string _connectionstring;
        public BillDetailsRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<BillDetailsModel> SelectAll()
        {
            var billDetails = new List<BillDetailsModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_BillDetails_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    billDetails.Add(new BillDetailsModel
                    {
                        BillDetailID = Convert.ToInt32(reader["BillDetailID"]),
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    });

                }
            }
            return billDetails;
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
                        BillInfo = reader["BillInfo"].ToString()
					});
				}
			}
			return bills;
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
		#region SelectByID
		public BillDetailsModel SelectByID(int billDetailsID)
        {
            BillDetailsModel billDetails = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_BillDetails_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("@BillDetailID", billDetailsID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    billDetails = new BillDetailsModel
                    {
                        BillDetailID = Convert.ToInt32(reader["BillDetailID"]),
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
						ProductName = reader["ProductName"].ToString(),
						Quantity = Convert.ToInt32(reader["Quantity"]),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return billDetails;
        }
        #endregion
        #region Insert
        public bool Insert(BillDetailsModel billsdetailsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_BillDetails_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@BillID", billsdetailsModel.BillID);
                cmd.Parameters.AddWithValue("@ProductID", billsdetailsModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", billsdetailsModel.Quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", billsdetailsModel.UnitPrice);
                cmd.Parameters.AddWithValue("@SubTotal", billsdetailsModel.SubTotal);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now); // Ensure @Created is provided
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(BillDetailsModel billsdetailsModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_BillDetails_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@BillID", billsdetailsModel.BillID);
                cmd.Parameters.AddWithValue("@BillDetailID", billsdetailsModel.BillDetailID);
                cmd.Parameters.AddWithValue("@ProductID", billsdetailsModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", billsdetailsModel.Quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", billsdetailsModel.UnitPrice);
                cmd.Parameters.AddWithValue("@SubTotal", billsdetailsModel.SubTotal);
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int billDetailsID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_BillDetails_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("@BillDetailID", billDetailsID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
    }
}
