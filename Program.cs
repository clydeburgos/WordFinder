using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordFinder
{

    public class WordFinder
    {
        IEnumerable<string> _dictionary;

        public WordFinder(IEnumerable<string> dictionary)
        {
            //construct the words inputted to an multidimensional array
            _dictionary = dictionary;
        }
        public IList<string> Find(IEnumerable<string> src)
        {
            //construct the matrix - multidimensional array
            IList<string> matchingWords = new List<string>();
            string[,] matrix = new string[src.FirstOrDefault().Length, src.Count()];
            for (int i = 0; i < src.Count(); i++)
            {
                string segment = src.ElementAt(i);
                for (int j = 0; j < segment.Length; j++)
                {
                    //append to the array
                    string letter = segment[j].ToString();
                    matrix[i, j] = letter;
                }
            }

            //deconstruct the matrix and make words
            //compare
            string horizontalWord = string.Empty;
            string verticalWord = string.Empty;

            foreach (string word in _dictionary)
            {
                //horizontal
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (j != matrix.GetLength(1))
                        {
                            horizontalWord += matrix[i, j];
                        }


                        var match = Regex.Match(horizontalWord, word);
                        if (match.Success && !matchingWords.Contains(match.Value))
                        {
                            string matched = match.Value;
                            matchingWords.Add(matched);
                        }
                      

                        if (horizontalWord.Length == matrix.GetLength(1))
                        {
                            horizontalWord = string.Empty; //clear
                        }

                    }
                }

                //vertical
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (j != matrix.GetLength(0))
                        {
                            verticalWord += matrix[j, i];
                        }

                        var match = Regex.Match(verticalWord, word);
                        if (match.Success && !matchingWords.Contains(match.Value))
                        {
                            string matched = match.Value;
                            matchingWords.Add(matched);
                        }

                        if (verticalWord.Length == matrix.GetLength(0))
                        {
                            verticalWord = string.Empty; //clear
                        }
                    }
                }

            }
            
            // look for the word

            return matchingWords;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            //word dictionary - input
            var dictionary = new string[] { "chill", "wind", "snow", "cold" };
            //matrix
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };

            WordFinder wordFinder = new WordFinder(dictionary);
            IEnumerable<string> wordsMatched = wordFinder.Find(src);
            foreach (string matchedWords in wordsMatched) {
                Console.WriteLine(matchedWords);
            }
            Console.Read();
        }
    }
}
