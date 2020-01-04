using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatMessageListDesignModel : ChatMessageListViewModel
    {
        #region Singleton
        public static ChatMessageListDesignModel Instance => new ChatMessageListDesignModel();
        #endregion
        #region Constructor
        public ChatMessageListDesignModel()
        {
            //User TestUser = new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto.");
            //ObservableCollection<Message> TestMessages = new ObservableCollection<Message>();
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "C:/Users/Владелец/source/repos/Chatee/ChateeWPF/Images/Samples/black-tea.png"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "D:/Test/TestFiles/Palermo Story.txt"));

            //Chat TestChat = new Chat(1, 1, 2, TestMessages);
            //ChatMessageListViewModel TestMessageList = new ChatMessageListViewModel(TestUser, TestChat);
        }
        #endregion
    }
}
