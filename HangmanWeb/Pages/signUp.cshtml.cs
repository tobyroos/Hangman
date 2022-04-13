using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace HangmanWeb.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<SignUpModel> _logger;
        public string username { get; set; }
        public string password { get; set; }

        public SignUpModel(ILogger<SignUpModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public void OnPost(Models.SignUpModel su)
        {
            string username = su.username;
            string password = su.password;

            string u = "";

            String connectionString = @"Data Source=C:\Users\tobyr\Hangman\HangmanWeb\db\hangmanDB2.db";

            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"SELECT username FROM Users WHERE username = $username";
                command.Parameters.AddWithValue("$username", username);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        u = reader.GetString(0);
                    }
                }

                if (u == "" && su.password != null)
                {
                    Random rand = new Random();
                    Byte[] b = new Byte[2];
                    rand.NextBytes(b);
                    string salt = "";
                    for (int i = 0; i < 2; i++)
                    {
                        salt += b[i].ToString("X");
                    }
                    password += salt;

                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                        byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                        string hashedPass = BitConverter.ToString(hashBytes).Replace("-", String.Empty);


                        var insCommand = conn.CreateCommand();
                        insCommand.CommandText = @"INSERT INTO Users (username, password, salt) VALUES ($username, $password, $salt)";
                        insCommand.Parameters.AddWithValue("$username", username);
                        insCommand.Parameters.AddWithValue("$password", hashedPass);
                        insCommand.Parameters.AddWithValue("$salt", salt);

                        var ins = insCommand.ExecuteNonQuery();

                        SessionVar.Username = username;
                        Response.Redirect("https://localhost:7249/game.html");
                    }
                }
            }
        }
    }
}