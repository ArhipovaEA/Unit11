using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Unit11
{
    public class AddWord : AbstractCommand
    {
        private readonly ITelegramBotClient botClient;

        private readonly Dictionary<long, Word> Buffer;

        public AddWord(ITelegramBotClient botClient)
        {
            Command = "/addword";

            this.botClient = botClient;

            Buffer = new Dictionary<long, Word>();
        }

        public async void StartProcessAsync(Conversation chat)
        {
            Buffer.Add(chat.GetId(), new Word());

            var text = "Введите русское значение слова";

            await SendCommandText(text, chat.GetId());
        }

        private async Task SendCommandText(string text, long chat)
        {
            await botClient.SendTextMessageAsync(chatId: chat, text: text);
        }

        public async void DoForStageAsync(AddState addingState, Conversation chat, string message)
        {
            var word = Buffer[chat.GetId()];
            var text = "";

            switch (addingState)
            {
                case AddState.Rus:
                    word.Rus = message;

                    text = "Введите английское значение слова";
                    break;

                case AddState.Eng:
                    word.Eng = message;

                    text = "Введите тематику";
                    break;

                case AddState.Theme:
                    word.Theme = message;

                    text = "Слово " + word.Eng + " добавлено в словарь. ";

                    chat.dictionary.Add(word.Rus, word);

                    Buffer.Remove(chat.GetId());
                    break;
            }


            await SendCommandText(text, chat.GetId());
        }
    }
}
