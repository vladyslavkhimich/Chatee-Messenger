using ChateeCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChateeCore
{
    public class ChatAttachmentPopupMenuViewModel : BasePopupMenuViewModel
    {
        #region Public Properties
        public ChatMessageListViewModel ParentChatMessageList { get; set; }
        public ObservableCollection<ChatMessageListItemFileAttachmentViewModel> SelectedFiles { get; set; }
        public bool IsAttachmentsListVisible { get; set; } 
        #endregion
        #region Constructors
        public ChatAttachmentPopupMenuViewModel(ChatMessageListViewModel parentChatMessageList)
        {
            ParentChatMessageList = parentChatMessageList;
            SelectedFiles = new ObservableCollection<ChatMessageListItemFileAttachmentViewModel>();
            Content = new MenuViewModel
            {
                Items = new List<MenuItemViewModel>(new[]
                {
                    new MenuItemViewModel { Type = MenuItemType.Header, Text="Attach a file..."},
                    new MenuItemViewModel { Icon = IconType.File , Text="From computer", Command = new RelayCommand(SelectFiles)  },
                })
            };
        }
        #endregion
        #region Helper Methods
        public void SelectFiles()
        {
            SelectedFiles = new ObservableCollection<ChatMessageListItemFileAttachmentViewModel>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files(*.*)|*.*|Image files(*.jpg *.jpeg *.png)|*.jpg;*.jpeg;*.png"
            };
            if((bool)openFileDialog.ShowDialog())
            {
                IsAttachmentsListVisible = true;
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                    if(new FileInfo(openFileDialog.FileNames[i]).Length < 209715201)
                        SelectedFiles.Add(new ChatMessageListItemFileAttachmentViewModel(openFileDialog.FileNames[i], ParentChatMessageList, ParentChatMessageList.Interlocutor));
                ParentChatMessageList.IsAttachmentMenuVisible = false;
            }
            
        }
        #endregion
    }
}
