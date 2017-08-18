namespace Login
{
    using System;

    internal class Program
    {
        private static bool CheckLogin(string logonPage, LoginService service, LoginInformation loginInformation)
        {
            try
            {
                string response = service.Login(loginInformation, logonPage);

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("unable to login");
                    return false;
                }
                else
                {
                    Console.WriteLine($"response message: {response}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User is unauthorized: {ex.Message}");
                return false;
            }
        }

        private static void Main(string[] args)
        {
            const string logonPage = "http://localhost/login.php";
            LoginInformation validLoginInformation = new LoginInformation("TestUser", "SecurePa$$");
            LoginInformation invalidLoginInformation = new LoginInformation("Invalid", "hackingRootPassword");

            LoginService service = new LoginService();

            // Test valid login
            Console.WriteLine("Valid Login results in:");
            bool loginFirstUser = Program.CheckLogin(logonPage, service, validLoginInformation);

            Console.WriteLine();

            // Test invalid login
            Console.WriteLine("Invalid Login results in:");
            bool loginSecoundUser = Program.CheckLogin(logonPage, service, invalidLoginInformation);

            Console.ReadLine();
        }
    }
}
