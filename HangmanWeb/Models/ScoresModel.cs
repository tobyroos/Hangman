using Microsoft.AspNetCore.Mvc;

namespace HangmanWeb.Models
{
    public class ScoresModel
    {
        [BindProperty]
        public int id { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public int score { get; set; }

    }
}
