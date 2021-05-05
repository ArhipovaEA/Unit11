using System;
using System.Collections.Generic;
using System.Text;

namespace Unit11
{
    public class AddControl
    {
        private readonly Dictionary<long, AddState> ChatAdding;

        public AddState GetStage(Conversation chat)
        {
            return ChatAdding[chat.GetId()];
        }

        public AddControl()
        {
            ChatAdding = new Dictionary<long, AddState>();
        }

        public void AddFirstState(Conversation chat)
        {
            ChatAdding.Add(chat.GetId(), AddState.Rus);
        }

        public void NextStage( Conversation chat)
        {
            var currentstate = ChatAdding[chat.GetId()];
            ChatAdding[chat.GetId()] = currentstate + 1;

            if (ChatAdding[chat.GetId()] == AddState.Finish)
            {
                chat.IsAddingInProcess = false;
                ChatAdding.Remove(chat.GetId());
            }
        }
    }
}
