using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public abstract class ChatTextCommandOption : AbstractCommand
    {
        public override bool CheckMessage(string message)
        {
            return message.StartsWith(Command);
        }

        public string ClearMessageFromCommand(string message)
        {
            return message.Substring(Command.Length + 1);
        }

    }
}
