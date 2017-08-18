namespace Login
{
    public class LoginInformation
    {
        public LoginInformation(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}
