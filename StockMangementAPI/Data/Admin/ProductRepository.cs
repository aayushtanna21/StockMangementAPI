using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
                        ProductImage = reader["ProductImage"] != DBNull.Value ? $"http://localhost:5165{reader["ProductImage"].ToString()}" : null,
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
        public List<CategoryDropDownModel> CategoryDropDown()
        {
            var category = new List<CategoryDropDownModel>();
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
                    category.Add(new CategoryDropDownModel()
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
                        ProductImage = reader["ProductImage"] != DBNull.Value ? $"http://localhost:5165{reader["ProductImage"].ToString()}" : null,
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
                // Save image to the server and store its path
                if (productModel.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages");
                    Directory.CreateDirectory(uploadsFolder); // Ensure directory exists
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productModel.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        productModel.ImageFile.CopyTo(fileStream);
                    }

                    productModel.ProductImage = "/ProductImages/" + uniqueFileName; // Store path instead of Base64
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

                // 🛠 Handle Image Update (Only update if new image is provided)
                if (productModel.ImageFile != null)
                {
                    string imagePath = ImageHelper.ConvertImageToBase64(productModel.ImageFile);
                    productModel.ProductImage = imagePath;
                }

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
        #region Price Drop Down
        public IEnumerable<decimal> GetPriceDropDown()
        {
            List<decimal> prices = new List<decimal>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Price FROM Product ORDER BY Price ASC", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    prices.Add(Convert.ToDecimal(reader["Price"]));
                }
            }
            return prices;
        }

        #endregion
        //#region Product filter
        //public IEnumerable<ProductModel> GetFilteredProducts(string categoryName, decimal? price, string customerName)
        //{
        //    List<ProductModel> products = new List<ProductModel>();
        //    using (SqlConnection conn = new SqlConnection(_connectionstring))
        //    {
        //        SqlCommand cmd = new SqlCommand("GetProductsByFilter", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@CategoryName", string.IsNullOrEmpty(categoryName) ? (object)DBNull.Value : categoryName);
        //        cmd.Parameters.AddWithValue("@Price", price ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@CustomerName", string.IsNullOrEmpty(customerName) ? (object)DBNull.Value : customerName);

        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            products.Add(new ProductModel
        //            {
        //                ProductID = Convert.ToInt32(reader["ProductID"]),
        //                ProductName = reader["ProductName"].ToString(),
        //                ProductImage = reader["ProductImage"].ToString(),
        //                CategoryName = reader["CategoryName"].ToString(),
        //                StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
        //                Price = Convert.ToDecimal(reader["Price"]),
        //                CostPrice = Convert.ToDecimal(reader["CostPrice"]),
        //                ReorderLevel = Convert.ToInt32(reader["ReorderLevel"]),
        //                Unit = reader["Unit"].ToString(),
        //                CustomerName = reader["CustomerName"].ToString(),
        //                Created = Convert.ToDateTime(reader["Created"]),
        //                Modified = Convert.ToDateTime(reader["Modified"])
        //            });
        //        }
        //    }
        //    return products;
        //}

        //#endregion

        public async Task<List<CategoryDropDownModel>> GetCategoryNames()
        {
            List<CategoryDropDownModel> categories = new List<CategoryDropDownModel>();

            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("GetCategoryNames", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        categories.Add(new CategoryDropDownModel
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }
                }
            }
            return categories;
        }

        public async Task<List<decimal>> GetDistinctPrices()
        {
            List<decimal> prices = new List<decimal>();

            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("GetDistinctPrices", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        prices.Add(Convert.ToDecimal(reader["Price"]));
                    }
                }
            }
            return prices;
        }

        public async Task<List<CustomersDropDownModel>> GetCustomerNames()
        {
            List<CustomersDropDownModel> customers = new List<CustomersDropDownModel>();

            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("GetCustomerNames", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        customers.Add(new CustomersDropDownModel
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerName = reader["CustomerName"].ToString()
                        });
                    }
                }
            }
            return customers;
        }

        public List<ProductModel> FilterProducts(int? categoryID, decimal? price, int? customerID, DateTime? createdDate)
        {
            List<ProductModel> products = new List<ProductModel>();

            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("GetProductsByFilter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryID", (object)categoryID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", price.HasValue ? (object)price.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CustomerID", (object)customerID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedDate", createdDate.HasValue ? (object)createdDate.Value.Date : DBNull.Value);

                    con.Open();
                    SqlDataReader reader =  cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new ProductModel
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            ProductImage = reader["ProductImage"] != DBNull.Value ? $"http://localhost:5165{reader["ProductImage"].ToString()}" : null,
                            CategoryName = reader["CategoryName"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                            CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                            ReorderLevel = Convert.ToInt32(reader["ReorderLevel"]),
                            Unit = reader["Unit"].ToString(),
                            Created = Convert.ToDateTime(reader["Created"]),
                            Modified = Convert.ToDateTime(reader["Modified"])
                        });
                    }
                }
            }
            return products;
        }

    }
}
