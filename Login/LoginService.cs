namespace Login
{
    using System.IO;
    using System.Net;
    using System.Text;

    internal class LoginService
    {
        public string Login(LoginInformation loginInformation, string loginUrl)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(loginUrl);
            req.AllowAutoRedirect = true;
            string values = "username=" + loginInformation.Username + "&sha_pass_hash=" + loginInformation.Password;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = values.Length;
            CookieContainer a = new CookieContainer();
            req.CookieContainer = a;

            ServicePointManager.Expect100Continue = false;

            StreamWriter writer = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            writer.Write(values);
            writer.Close();

            req.Timeout = 5000;
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            if (res.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            Stream resp = res.GetResponseStream();
            StreamReader reader = new StreamReader(resp, Encoding.GetEncoding(res.CharacterSet));
            string response = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            res.Dispose();

            return response;
        }
    }
}
