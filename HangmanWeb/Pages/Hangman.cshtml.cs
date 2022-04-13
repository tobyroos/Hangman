using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;

namespace HangmanWeb.Pages
{
    public class HangmanModel : PageModel
    {
        private readonly ILogger<HangmanModel> _logger;
        public char guess { get; set; }
        public List<char> previousGuesses { get; set; }

        public HangmanModel(ILogger<HangmanModel> logger)
        {
            _logger = logger;
        }
        public void OnSet()
        {

        }
        public void OnGet()
        {

        }

        public void OnPost(Models.HangmanModel hangman)
        {
            
        }
    }
}