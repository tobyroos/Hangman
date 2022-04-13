using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace HangmanWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string username { get; set; }
        public string password { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public void OnPost(Models.IndexModel index)
        {
            string username = index.username;
            string password = index.password;
            Random rand = new Random();
            Byte[] b = new Byte[2];
            rand.NextBytes(b);
            string salt = "";
            for (int i = 0; i < 2; i++)
            {
                salt += b[i].ToString("X");
            }

            password += salt;

            string u = "";
            string p = "";
            string s = "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hashedPass = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                //C:\Users\tobyr\Documents\CS_3750\HangmanProject3750-main\HangmanWeb\HangmanWeb\db\hangmanDB.db
                //C:\Users\codyb\Documents\Computer Science\SE2\HangmanWeb\HangmanWeb\db\hangmanDB.db

                String connectionString = @"Data Source=C:\Users\tobyr\Hangman\HangmanWeb\db\hangmanDB2.db";

                using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    conn.Open();

                    var command = conn.CreateCommand();
                    command.CommandText = @"SELECT username, password, salt FROM Users WHERE username = $username";
                    command.Parameters.AddWithValue("$username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            u = reader.GetString(0);
                            p = reader.GetString(1);
                            s = reader.GetString(2);
                        }
                    }
                }

                byte[] sourceBytes1 = Encoding.UTF8.GetBytes(index.password + s);
                byte[] hashBytes1 = sha256Hash.ComputeHash(sourceBytes1);
                string hashedPass1 = BitConverter.ToString(hashBytes1).Replace("-", String.Empty);

                if (hashedPass1 == p)
                {
                    SessionVar.Username = username;
                    Response.Redirect("https://localhost:7249/game.html");
                }
            }
        }

    }


    public class SessionVar
    {
        public static string username;
        public static String Username { get; set; }
        public static int score;
        public static int Score { get; set; }
    }
}