using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data
{
    public class ProductRepository
    {
        private readonly string _connectionstring;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("StockMangmentDB");
        }
        #region SelectAll
        public IEnumerable<ProductModel> SelectAll()
        {
            var product = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_SelectAll", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.Add(new ProductModel
                    {
                        ProductID=Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
						ProductImage = reader["ProductImage"].ToString(),
						CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                        ReorderLevel = Convert.ToInt32(reader["ReorderLevel"]),
                        Unit = reader["Unit"].ToString(),
						CustomerID = Convert.ToInt32(reader["CustomerID"]),
						CustomerName = reader["CustomerName"].ToString(),
						Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    });

                }
            }
            return product;
        }
		#endregion
		#region CategoryDropDown
		public List<CategoryModel> CategoryDropDown()
        {
            var category= new List<CategoryModel>();
            using(SqlConnection conn=new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Category_DropDown",conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    category.Add(new CategoryModel()
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
            return category;
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
		#region SelectByID
		public ProductModel SelectByID(int productID)
        {
            ProductModel product = null;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_SelectAllByID", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("ProductID", productID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product = new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
						ProductImage = reader["ProductImage"].ToString(),
						CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                        ReorderLevel = Convert.ToInt32(reader["ReorderLevel"]),
                        Unit = reader["Unit"].ToString(),
						CustomerID = Convert.ToInt32(reader["CustomerID"]),
						CustomerName = reader["CustomerName"].ToString(),
						Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                }
            }
            return product;
        }
        #endregion
        #region Insert
        public bool Insert(ProductModel productModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Insert", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
				cmd.Parameters.AddWithValue("@ProductImage", productModel.ProductImage);
				cmd.Parameters.AddWithValue("@CategoryID", productModel.CategoryID);
                cmd.Parameters.AddWithValue("@StockQuantity", productModel.StockQuantity);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@CostPrice", productModel.CostPrice);
                cmd.Parameters.AddWithValue("@ReorderLevel", productModel.ReorderLevel);
                cmd.Parameters.AddWithValue("@Unit", productModel.Unit);
				cmd.Parameters.AddWithValue("@CustomerID", productModel.CustomerID); 
                cmd.Parameters.AddWithValue("@Created", DateTime.Now); // Ensure @Created is provided
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Update
        public bool Update(ProductModel productModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Update", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                cmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
				cmd.Parameters.AddWithValue("@ProductImage", productModel.ProductImage);
				cmd.Parameters.AddWithValue("@CategoryID", productModel.CategoryID);
                cmd.Parameters.AddWithValue("@StockQuantity", productModel.StockQuantity);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@CostPrice", productModel.CostPrice);
                cmd.Parameters.AddWithValue("@ReorderLevel", productModel.ReorderLevel);
                cmd.Parameters.AddWithValue("@Unit", productModel.Unit);
				cmd.Parameters.AddWithValue("@CustomerID", productModel.CustomerID); 
                cmd.Parameters.Add("@Modified", SqlDbType.DateTime).Value = DBNull.Value;
                conn.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                return rowsaffected > 0;
            }

        }
        #endregion
        #region Delete
        public bool Delete(int productID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Delete", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                };
                cmd.Parameters.AddWithValue("ProductID", productID);
                conn.Open();
                int rowaffected = cmd.ExecuteNonQuery();
                return rowaffected > 0;
            }
        }
        #endregion
    }
}
