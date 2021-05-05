using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    interface IChatTextCommandWithAction : IChatTextCommand
    {
        bool DoAction(Conversation chat);
    }
}
