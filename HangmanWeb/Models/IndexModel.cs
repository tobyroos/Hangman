using Microsoft.AspNetCore.Mvc;

namespace HangmanWeb.Models
{
    public class IndexModel
    {
        [BindProperty]
        public int id { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public string password { get; set; }

        [BindProperty]
        public string salt { get; set; }
    }
}
