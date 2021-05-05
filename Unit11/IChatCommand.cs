using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public interface IChatCommand
        {
            bool CheckMessage(string message);
        }
   
}
