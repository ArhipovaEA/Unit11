using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public class SayHi : AbstractCommand, IChatTextCommand
    {
        public SayHi()
        {
            Command = "/sayhi";
        }

        public string ReturnText()
        {
            return "Привет";
        }

    }
}
