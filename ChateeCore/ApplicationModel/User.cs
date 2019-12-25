using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class User
    {
        #region Public Properties
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Initials { get; set; }
        public string ProfilePictureRGB { get; set; }
        public string LastMessage { get; set; }
        public ObservableCollection<Chat> Chats { get; set; }
        #endregion
        #region Constructors
        public User()
        {
            Chats = new ObservableCollection<Chat>();
        }
        public User(WCF_Server.DataContracts.UserContract userContract)
        {
            
            UserID = userContract.UserID;
            Username = userContract.Username;
            Email = userContract.Email;
            Name = userContract.Name;
            Bio = userContract.Bio;
            Initials = userContract.Initials;
            ProfilePictureRGB = userContract.ProfilePictureRGB;
            Chats = new ObservableCollection<Chat>();
            if (userContract.Chats != null)
                foreach (var chat in userContract.Chats)
                    Chats.Add(new Chat(chat));
        }
        public User(int userID, string username, string email, string name, string bio, string initials, string profilePictureRGB, string lastMessage, ObservableCollection<Chat> chats = null)
        {
            UserID = userID;
            Username = username;
            Email = email;
            Name = name;
            Bio = bio;
            Initials = initials;
            ProfilePictureRGB = profilePictureRGB;
            LastMessage = lastMessage;
            if(chats != null)
               Chats = new ObservableCollection<Chat>(chats);
        }
        public User(User user)
        {
            UserID = user.UserID;
            Username = user.Username;
            Email = user.Email;
            Name = user.Name;
            Bio = user.Bio;
            Initials = user.Initials;
            ProfilePictureRGB = user.ProfilePictureRGB;
            LastMessage = user.LastMessage;
            Chats = new ObservableCollection<Chat>(user.Chats);
        }
        #endregion
    }
}
