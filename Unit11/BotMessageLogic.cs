using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Unit11
{
    class BotMessageLogic
    {
        private readonly Messenger messanger;
        private readonly ITelegramBotClient botClient;

        private readonly Dictionary<long, Conversation> ChatList;

        public BotMessageLogic(ITelegramBotClient botClient)
        {
            messanger = new Messenger(botClient);
            ChatList = new Dictionary<long,Conversation>();
            this.botClient = botClient;
        }

        private async Task SendMessage(Conversation chat)
        {
            await messanger.MakeAnswer(chat);

        }


        public async Task Response(MessageEventArgs e)
        {
            var Id = e.Message.Chat.Id;

            if (!ChatList.ContainsKey(Id))
            {
                var newchat = new Conversation(e.Message.Chat);

                ChatList.Add(Id, newchat);
            }

            var chat = ChatList[Id];

            chat.AddMessage(e.Message);

            await SendMessage(chat);

        }
    }
}
