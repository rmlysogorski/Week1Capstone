using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            PigLatin P = new PigLatin();
            bool runAgain = true;
            string welcomeMessage = "Welcome to the Pig Latin Translator!";
            string continueMessage = "Translate another line? (y/n): ";
            string goodbyeMessage = "Thanks! See you next time!";

            P.PrintInColor(welcomeMessage, ConsoleColor.Green);

            while (runAgain)
            {                
                P.GetPigLatin();
                runAgain = P.EndProgram(continueMessage, "y", "n");
            }

            P.PrintInColor(P.Translate(goodbyeMessage), ConsoleColor.Green);
        }
    }
}
