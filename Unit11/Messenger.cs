using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Unit11
{
    public class Messenger
    {
        private readonly ITelegramBotClient botClient;
        
        private readonly Parser parser;

        public Messenger(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            parser = new Parser();
            RegisterCommand();
          
        }

        private void RegisterCommand()
        {
            parser.AddCommand(new AddWord(botClient));
            parser.AddCommand(new Training(botClient));
            parser.AddCommand(new SayHi());
            parser.AddCommand(new DeleteWord());
            parser.AddCommand(new StopTraining());
            parser.AddCommand(new Dictionary());

        }

        public async Task MakeAnswer(Conversation chat)
        {
            var lastmessage = chat.GetLastMessage();

            if (chat.IsTraningInProcess && !parser.IsTextCommand(lastmessage))
            {
                parser.ContinueTraining(lastmessage, chat);

                return;
            }

            if (chat.IsAddingInProcess)
            {
                parser.NextStage(lastmessage, chat);

                return;
            }

            if (parser.IsMessageCommand(lastmessage))
            {
                await ExecCommand(chat, lastmessage);
            }
            else
            {
                var text = CreateTextMessage();

                await SendText(chat, text);
            }

        }

        private async Task SendText(Conversation chat, string text)
        {
            await botClient.SendTextMessageAsync(
                  chatId: chat.GetId(),
                  text: text
                );
        }

        private string CreateTextMessage()
        {
            var text = "Пусто";

            return text;
        }

        public async Task ExecCommand(Conversation chat, string command)
        {
            if (parser.IsTextCommand(command))
            {
                var text = parser.GetMessageText(command, chat);

                await SendText(chat, text);
            }

            if (parser.IsButtonCommand(command))
            {
                var keys = parser.GetKeyBoard(command);
                var text = parser.GetInformationalMeggase(command);
                parser.AddCallback(command, chat);

                await SendTextWithKeyBoard(chat, text, keys);

            }

            if (parser.IsAddingCommand(command))
            {
                chat.IsAddingInProcess = true;
                parser.StartAddingWord(command, chat);
            }
        }

        private async Task SendTextWithKeyBoard(Conversation chat, string text, InlineKeyboardMarkup keyboard)
        {
            await botClient.SendTextMessageAsync( chatId: chat.GetId(), text: text, replyMarkup: keyboard );
        }

    }
}
