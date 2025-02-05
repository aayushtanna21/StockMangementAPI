namespace StockMangementAPI.Models
{
	#region Category Model
	public class CategoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
	#endregion
}
