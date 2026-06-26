namespace PersonalFinanceApp
{
    public class ProfileSetting
    {
        public int Id { get; set; }
        public string PreferredCurrency { get; set; } = "UAH";
        public bool EnableNotifications { get; set; }
         
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}