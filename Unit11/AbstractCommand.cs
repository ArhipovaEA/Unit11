using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public abstract class AbstractCommand : IChatCommand
    {
        public string Command;

        public virtual bool CheckMessage(string message)
        {
            return Command == message;
        }
    }
}
