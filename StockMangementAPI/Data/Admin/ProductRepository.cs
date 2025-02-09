using Microsoft.Data.SqlClient;
using StockMangementAPI.Models;
using System.Data;

namespace StockMangementAPI.Data.Admin
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
            var products = new List<ProductModel>();
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
                    products.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductImage = "/ProductImages/" + reader["ProductImage"].ToString(), // ✅ Returns full image path
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
            return products;
        }

        #endregion
        #region CategoryDropDown
        public List<CategoryModel> CategoryDropDown()
        {
            var category = new List<CategoryModel>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("PR_Category_DropDown", conn);
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
                        ProductImage = "/ProductImages/" + reader["ProductImage"].ToString(),
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
                SqlCommand cmd = new SqlCommand("PR_Product_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Convert image to Base64 and store in DB
                if (productModel.ImageFile != null)
                {
                    string base64Image = ImageHelper.ConvertImageToBase64(productModel.ImageFile);
                    productModel.ProductImage = base64Image;
                }

                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@ProductImage", (object)productModel.ProductImage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CategoryID", productModel.CategoryID);
                cmd.Parameters.AddWithValue("@StockQuantity", productModel.StockQuantity);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@CostPrice", productModel.CostPrice);
                cmd.Parameters.AddWithValue("@ReorderLevel", productModel.ReorderLevel);
                cmd.Parameters.AddWithValue("@Unit", productModel.Unit);
                cmd.Parameters.AddWithValue("@CustomerID", productModel.CustomerID);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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

                //// 🛠 Handle Image Update (Only update if new image is provided)
                //if (productModel.ImageFile != null)
                //{
                //	string imagePath = ImageHelper.SaveImageToFile(productModel.ImageFile);
                //	productModel.ProductImage = imagePath;
                //}

                cmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@ProductImage", productModel.ProductImage ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@CategoryID", productModel.CategoryID);
                cmd.Parameters.AddWithValue("@StockQuantity", productModel.StockQuantity);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@CostPrice", productModel.CostPrice);
                cmd.Parameters.AddWithValue("@ReorderLevel", productModel.ReorderLevel);
                cmd.Parameters.AddWithValue("@Unit", productModel.Unit);
                cmd.Parameters.AddWithValue("@CustomerID", productModel.CustomerID);
                cmd.Parameters.AddWithValue("@Modified", DateTime.Now);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
