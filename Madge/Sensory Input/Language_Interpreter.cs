using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madge.Sensory_Input
{
    class Language_Interpreter
    {
        public bool CheckForAffirmative(string userInput)
        {
            //Possible affirmitive responses
            String[] affirmResponse = { "yes", "y", "yeah", "yea", "ok", "sure","alright","dig","yep","yay","yayer","oui","si","ja","hai","gee","haan","shi","naam","da"};

            //breakout each word and lowcase the input for ease of translation
            string[] words = userInput.ToLower().Split(' ');
            //check each word to see if there is a match for an affirmative. Return true if one is detected.
            foreach (string word in words)
            {
                if (Array.Exists(words, element => element == word)) { return true; }
            }
            //if we made it this far, they didn't say anything affirmative
            return false;
        }
    }
}
