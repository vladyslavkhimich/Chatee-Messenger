using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatListViewModel : BaseViewModel
    {
        public ObservableCollection<ChatListItemViewModel> Chats { get; set; }

        public ChatListViewModel()
        {

        }
    }
}
