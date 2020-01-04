using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Server
{
    public class DataContracts
    {
        [DataContract]
        public class UserContract
        {
            [DataMember]
            [Key]
            public int UserID { get; set; }
            [DataMember]
            public string Username { get; set; }
            [DataMember]
            public string Email { get; set; }
            [DataMember]
            public byte[] PasswordHash { get; set; }
            [DataMember]
            public byte[] AvatarBytes { get; set; }
            [DataMember]
            public string AvatarCheckSum { get; set; }
            [DataMember]
            public string AvatarFileName { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Bio { get; set; }
            [DataMember]
            public string Initials { get; set; }
            [DataMember]
            public string ProfilePictureRGB { get; set; }
            [IgnoreDataMember]
            public bool IsKeepLoggedIn { get; set; }
            [IgnoreDataMember]
            public int ServerDatabaseUserID { get; set; }
            [DataMember]
            [NotMapped]
            public ObservableCollection<ChatContract> Chats { get; set; }
            public UserContract()
            {

            }
            public UserContract(WCF_Server.DataContracts.UserContract userContract)
            {
                UserID = userContract.UserID;
                Username = userContract.Username;
                Email = userContract.Email;
                PasswordHash = userContract.PasswordHash;
                AvatarBytes = userContract.AvatarBytes;
                AvatarCheckSum = userContract.AvatarCheckSum;
                AvatarFileName = userContract.AvatarFileName;
                Name = userContract.Name;
                Bio = userContract.Bio;
                Initials = userContract.Initials;
                ProfilePictureRGB = userContract.ProfilePictureRGB;
                Chats = new ObservableCollection<ChatContract>();
                if (userContract.Chats != null)
                    foreach (var chat in userContract.Chats)
                        Chats.Add(chat);
            }
            public UserContract(WCF_Server.DataContracts.UserContract userContract, bool isKeepLoggedIn)
            {
                ServerDatabaseUserID = userContract.UserID;
                Username = userContract.Username;
                Email = userContract.Email;
                PasswordHash = userContract.PasswordHash;
                Name = userContract.Name;
                Bio = userContract.Bio;
                Initials = userContract.Initials;
                ProfilePictureRGB = userContract.ProfilePictureRGB;
                IsKeepLoggedIn = isKeepLoggedIn;
                Chats = new ObservableCollection<ChatContract>();
                if (userContract.Chats != null)
                    foreach (var chat in userContract.Chats)
                        Chats.Add(chat);
            }
        }
        [DataContract]
        public class ChatContract
        {
            [DataMember]
            [Key]
            public int ChatID { get; set; }
            [IgnoreDataMember]
            public int ServerDatabaseChatID { get; set; }
            [IgnoreDataMember]
            [NotMapped]
            public bool IsHasNewMessages { get; set; }
            [DataMember]
            public int UserID1 { get; set; }
            [DataMember]
            public int UserID2 { get; set; }
            [DataMember]
            [NotMapped]
            public ObservableCollection<MessageContract> Messages { get; set; }
            public ChatContract()
            {

            }
            public ChatContract(int userID1, int userID2)
            {
                UserID1 = userID1;
                UserID2 = userID2;
                Messages = new ObservableCollection<MessageContract>();
            }
            public ChatContract(int userID1, int userID2, int serverDatabaseChatID, ObservableCollection<MessageContract> messages)
            {
                UserID1 = userID1;
                UserID2 = userID2;
                ServerDatabaseChatID = serverDatabaseChatID;
                Messages = new ObservableCollection<MessageContract>(messages);
            }
        }
        [DataContract]
        public class MessageContract
        {
            [DataMember]
            [Key]
            public int MessageID { get; set; }
            [DataMember]
            public int ChatID { get; set; }
            [DataMember]
            public int UserID { get; set; }
            [DataMember]
            public string MessageText { get; set; }
            [DataMember]
            public DateTime MessageReadTime { get; set; }
            [DataMember]
            public DateTime MessageSentTime { get; set; }
            [DataMember]
            public string FileCheckSum { get; set; }
            [DataMember]
            public byte[] FileBytes { get; set; }
            [DataMember]
            public string FileName { get; set; }
            public MessageContract()
            {

            }
        }
        [DataContract]
        public class FileContract
        {
            [DataMember]
            public string FileName { get; set; }
            [DataMember]
            public string FileCheckSum { get; set; }
            [DataMember]
            public byte[] FileData { get; set; }
            [DataMember]
            public int UserID { get; set; }
            [DataMember]
            public int ChatID { get; set; }
            [DataMember]
            public bool IsNewFile { get; set; }
        }
    }
}
