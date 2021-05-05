using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public class StopTraining : AbstractCommand, IChatTextCommandWithAction
    {
        public StopTraining()
        {
            Command = "/stop";
        }

        public bool DoAction(Conversation chat)
        {
            chat.IsTraningInProcess = false;
            return !chat.IsTraningInProcess;
        }

        public string ReturnText()
        {
            return "Тренировка остановлена!";
        }
    }
}
