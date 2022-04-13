using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWeb
{
    public class HangmanGame
    {
        private char[] letters;
        private char[] guessing;
        private ArrayList previousGuesses = new ArrayList();
        private int lives;
        public HangmanGame()
        {
            lives = 0;
        }
        public void SetWord()
        {
            string fileName = "hangman.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string[] lines = File.ReadAllLines(path);
            Random rand = new Random();
            letters = lines[rand.Next(lines.Length)].ToCharArray();
            guessing = new char[letters.Length];
            for (int i = 0; i < letters.Length; i++)
            {
                guessing[i] = '-';
            }
        }

        public char[] CheckLetter(string s)
        {
            char guess = char.Parse(s);

            bool isCorrect = false;

            for (int i = 0; i < guessing.Length; i++)
            {
                if (guess == letters[i])
                {
                    guessing[i] = guess;
                    isCorrect = true;
                }

            }

            if (isCorrect == false)
            {
                SetLives();
                previousGuesses.Add(guess + " ");
            }
            return guessing;
        }

        public void SetLives()
        {
            lives += 1;
        }

        public int GetLives()
        {
            return lives;
        }

        public char[] GetDisplayLetters()
        {
            return guessing;
        }

        public bool CheckResult()
        {
            if (guessing.SequenceEqual(letters))
            {
                return true;
            }
            return false;
        }

        public void PrintPreviousGuesses()
        {
            foreach (var guess in previousGuesses)
            {
                Console.Write(guess);
            }
            Console.WriteLine();
        }
    }
}
