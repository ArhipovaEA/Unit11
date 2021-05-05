using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Unit11
{
    interface IKeyBoard
    {
        void AddCallBack(Conversation caht);

        InlineKeyboardMarkup ReturnKeyBoard();
        string InformationalMessage();
    }
}
