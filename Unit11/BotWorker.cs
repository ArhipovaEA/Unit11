using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Unit11
{
    public class BotWorker
    {
        private ITelegramBotClient botClient;
        private BotMessageLogic logic;

        public void Inizalize()
        {
            botClient = new TelegramBotClient(BotCredentials.BotToken);
            logic = new BotMessageLogic(botClient);
        }

        public void Start()
        {
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
        }

        public void Stop()
        {
            botClient.StopReceiving();
        }
        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
               await logic.Response(e);
            }
        }

    }

  
}
