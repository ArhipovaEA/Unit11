using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Unit11
{
    class Parser
    {
        private readonly List<IChatCommand> Command;
        private readonly AddControl addingController;

        public Parser()
        {
            Command = new List<IChatCommand>();
            addingController = new AddControl();
        }

        public void AddCommand(IChatCommand chatCommand)
        {
            Command.Add(chatCommand);
        }

        public bool IsMessageCommand(string message)
        {
            return Command.Exists(x => x.CheckMessage(message));
        }

        public bool IsButtonCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IKeyBoard;
        }
        public bool IsTextCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IChatTextCommand;
        }

        public void ContinueTraining(string message, Conversation chat)
        {
            var command = Command.Find(x => x is Training) as Training;

            command.NextStepAsync(chat, message);

        }

        public string GetMessageText(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IChatTextCommand;

            if (command is IChatTextCommandWithAction)
            {
                if (!(command as IChatTextCommandWithAction).DoAction(chat))
                {
                    return "Ошибка выполнения команды!";
                };
            }

            return command.ReturnText();
        }

        public void NextStage(string message, Conversation chat)
        {
            var command = Command.Find(x => x is AddWord) as AddWord;

            command.DoForStageAsync(addingController.GetStage(chat), chat, message);

            addingController.NextStage(chat);

        }

        public InlineKeyboardMarkup GetKeyBoard(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoard;

            return command.ReturnKeyBoard();
        }

        public string GetInformationalMeggase(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoard;

            return command.InformationalMessage();
        }

        public void AddCallback(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoard;
            command.AddCallBack(chat);
        }

        public bool IsAddingCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is AddWord;
        }

        public void StartAddingWord(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as AddWord;

            addingController.AddFirstState(chat);
            command.StartProcessAsync(chat);

        }
    }

}
