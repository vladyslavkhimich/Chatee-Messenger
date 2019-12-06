using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class ChatMessageListViewModel : BaseViewModel
    {
        #region Public Properties
        public List<ChatMessageListItemViewModel> Chats { get; set; }
        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }
        public bool IsAttachmentMenuVisible { get; set; } = false;
        #endregion
        #region Public Commands
        public ICommand AttachmentButtonCommand { get; set; }
        #endregion
        #region Constructors
        public ChatMessageListViewModel()
        {
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }
        #endregion
        #region Commands methods
        public void AttachmentButton()
        {
            IsAttachmentMenuVisible ^= true;
        }
        #endregion
    }
}
