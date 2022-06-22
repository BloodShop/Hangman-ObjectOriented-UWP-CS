using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Text;

namespace HangmanProject
{
    internal class Word : IPoint
    {
        // Property signatures:
        public List<TextBlock> TextBlocks { get; private set; }
        public char[] arrWord { get; private set; }
        public int Length { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        // Indexers
        public int this[int i] { get { return arrWord[i]; } }
        public bool this[char c] { get {
                for (int i = 0; i < Length; i++)
                    if(arrWord[i] == c)
                        return true;
                return false; } 
        }
        
        // Ctor
        public Word(Rank rank)
        {
            arrWord = GetRandomWordByRank(rank).ToCharArray();
            Length = arrWord.Length;
        }
        
        // Methods
        private string GetRandomWordByRank(Rank rank = Rank.Begginer) // rank is defined by word difficulty
        { // bank words
            Random rnd = new Random();
            switch (rank)
            {
                case Rank.Begginer:
                    string[] easy = { "day", "love", "rat", "cat", "dog", "deer", "monkey", "ape", "bat", "whale", "glass", "human", "bottle", "water", "animal", "prince" };
                    return easy[rnd.Next(0, easy.Length)].ToUpper();
                case Rank.Proffesional:
                    string[] pro = { "polymorphism", "inheritance", "strings", "integers", "accessibility", "return", "reference", "out", "parse", "enumarable", "initialize", "yield", "abruptly", "awkward" };
                    return pro[rnd.Next(0, pro.Length)].ToUpper();
                default:
                    return "LudwigVanBeethoven".ToUpper();
            }
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (char c in arrWord)
                str.Append(c);
            return str.ToString();
        }
    }
}