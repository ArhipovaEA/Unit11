using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public class Dictionary : ChatTextCommandOption, IChatTextCommandWithAction
    {
        string dictionary = "";
        public Dictionary()
        {
            Command = "/dictionary";
            
        }

        
        public bool DoAction(Conversation chat)
        {

           
            dictionary = "Словарь:";
           
            foreach (KeyValuePair<string, Word> keyValue in chat.dictionary)
            {
                
                dictionary = String.Concat(dictionary, "\n");
                dictionary = String.Concat(dictionary, keyValue.Key);
            }
           
            return true;
           
        }

        public string ReturnText()
        {
           return dictionary;
        }
    }

}
