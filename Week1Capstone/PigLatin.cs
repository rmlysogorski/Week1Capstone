using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Capstone
{
    class PigLatin
    {
        public string GetUserInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if(input == string.Empty)
            {
                PrintInColor("Oops - you forgot to type something!", ConsoleColor.Red);                
                return GetUserInput(message);
            }
            return input;
        }

        public void GetPigLatin()
        {
            string userSentence = GetUserInput("Please enter a line to be translated: ");
            string pigLatin = Translate(userSentence);
            PrintResults(pigLatin);
        }

        public string Translate(string sentence)
        {
            string newWord = string.Empty, newSentence = string.Empty;

            //Split the sentence into words
            foreach (string word in sentence.Split())
            {
                //check to see if the word has any numbers/symbols 
                //if there is a number/symbol don't change the word
                if (CheckNumsAndSyms(word))
                {
                    newWord = word;
                }
                //Otherwise, translate it into Pig Latin
                else
                {
                    newWord = GetNewWord(word, GetFirstVowelIndex(word));                    
                }
                //Put the new words into the new sentence
                newSentence += newWord + " ";
            }

            return newSentence;
        }

        public bool CheckNumsAndSyms(string word)
        {
            string numbersAndSymbols = "0123456789@#$%^&*()+=_\"\\/><";

            foreach (char c in numbersAndSymbols)
            {
                if (word.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetFirstVowelIndex(string word)
        {
            int index = word.Length;
            word = word.ToLower();
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

            //If the word has no vowels (like "my")
            //we'll treat y like a vowel
            if(!vowels.Any(word.Contains) && word.Contains('y'))
            {
                index = word.IndexOf('y');
            }
            //Otherwise it should check for vowels:
            else
            {
                foreach (char vowel in vowels)
                {
                    if (word.IndexOf(vowel) < index && word.IndexOf(vowel) != -1)
                    {
                        index = word.IndexOf(vowel);
                    }
                }
            }            

            return index;
        }

        public string GetNewWord(string word, int vowelIndex)
        {
            string newWord = string.Empty;
            string punctuation = ":!.,;?";
            string punctuationToAdd = string.Empty;
            int puncIndex = 0;         
            bool punc = false;

            //Check for punctuation at the end of the word
            //If it finds some remove it for now and
            //save it for later            
            if (punctuation.Any(word.Contains))
            {
                punc = true;
                for(int i = word.Length - 1; i > 0; i--)
                {
                    if (!punctuation.Any(word[i].ToString().Contains))
                    {
                        break;
                    }
                    else
                    {
                        puncIndex = i;
                    }
                }
                punctuationToAdd = word.Substring(puncIndex);
                word = word.Substring(0, puncIndex);
            }

            //Check the case and store it in an array of bools
            bool[] wordCase = DetermineCase(word);

            //Construct the new word depending
            //on where the first vowel is
            newWord = ConstructWord(word, vowelIndex);

            //Re-adds the punctuation if there was any
            if (punc)
            {
                newWord += punctuationToAdd;
            }

            //Redo the case based on the original word:
            newWord = RedoCase(wordCase, newWord, word);
            
            //Returns the new Pig Latin word
            return newWord;
        }

        public string RedoCase (bool[] wordCase, string newWord, string word)
        {
            if (wordCase[0] == true)
            {
                return newWord.ToUpper();
            }
            else
            {
                //Reset the case:
                newWord = newWord.ToLower();

                //Change the word into an array of chars:
                char[] wordArray = newWord.ToCharArray();

                //Change the case back according to how it was typed in:
                for (int i = 0; i < word.Length; i++)
                {
                    //Remember that wordCase[0] means it was all upper case
                    if (wordCase[i + 1] == true)
                    {
                        wordArray[i] = char.ToUpper(wordArray[i]);
                    }
                }
                //Put everything back into newWord:
                return new string(wordArray);
            }
        }

        public string ConstructWord(string word, int vowelIndex)
        {
            if (vowelIndex == 0)
            {
                return word + "way";
            }
            else
            {
                return word.Substring(vowelIndex) + word.Substring(0, vowelIndex) + "ay";
            }
        }

        public void PrintResults(string PigLatin)
        {            
            Console.Write("Here is your sentence in Pig Latin: ");
            PrintInColor(PigLatin, ConsoleColor.Yellow);
        }

        public bool EndProgram(string message, string opt1, string opt2)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if(input == opt1)
            {
                return true;
            }
            else if(input == opt2)
            {
                return false;
            }
            else
            {
                PrintInColor("Please enter 'y' for yes and 'n' for no.", ConsoleColor.Red);                
                return EndProgram(message, opt1, opt2);
            }
        }

        public bool GetIsUpper(string word)
        {
            //Stylistic change so that I is Iway and not IWAY
            if(word.Length == 1)
            {
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (char.IsLower(word[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool[] DetermineCase(string word)
        {
            bool[] array = new bool[word.Length + 1];
            //If the entire word is uppercase, store that in array[0]
            array[0] = GetIsUpper(word);

            //If the entire word is uppercase,
            //No need to find the case of each letter
            if (!array[0])
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (char.IsUpper(word[i]))
                    {
                        array[i + 1] = true;
                    }
                    else
                    {
                        array[i + 1] = false;
                    }
                }
            }
            
            return array;
        }

        public void PrintInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
