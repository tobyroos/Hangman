using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Data.SQLite;

namespace HangmanWeb.Pages
{
    public class ScoresModel : PageModel
    {
        private readonly ILogger<ScoresModel> _logger;
        public string username { get; set; }
        public int score { get; set; }

        public ScoresModel(ILogger<ScoresModel> logger)
        {
            _logger = logger;
        }

        public static string Createhtml()
        {

            SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\tobyr\Hangman\HangmanWeb\db\hangmanDB2.db");
            conn.Open();

            SQLiteCommand commandInsert = new SQLiteCommand("insert into Scores (username, score) VALUES ('" + SessionVar.Username + "', " + SessionVar.Score + ")", conn);
            commandInsert.ExecuteNonQuery();

            //.WriteLine(score.temp);

            string sql = "select username, score from Scores order by score LIMIT 10";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            string htmlTag = "<h3>Scores</h3>";
            int i = 1;

            while (reader.Read())
            {
                string currentUsername = Convert.ToString(reader["username"]);
                string currentScore = Convert.ToString(reader["score"]);
                //Console.WriteLine($"{currentUsername}, {currentScore}\n");
                htmlTag += "<p>" + i + "\t|\t" + currentUsername + "\t|\t" + currentScore + "</p>";
                i++;
            }

            reader.Close();

            return htmlTag;
        }

    }
}