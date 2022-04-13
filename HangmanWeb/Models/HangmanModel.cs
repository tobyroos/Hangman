using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HangmanWeb.Models
{
    public class HangmanModel
    {
        [BindProperty]
        public char guess { get; set; }

        [BindProperty]
        public List<char> previousGuesses { get; set; }
    }
}