namespace StockMangementAPI.Models
{
	#region User Model
	public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Created { get; set;}
        public DateTime? Modified { get; set;}
        public string? Role {  get; set; }
    }
	#endregion
	#region User DropDownModel
	public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
	#endregion
	#region User Login
	public class UserLoginModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
    #endregion
    #region Register
    public class UserRegistrationModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    #endregion
}
