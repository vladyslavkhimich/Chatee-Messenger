using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using static WCF_Server.DataContracts;

namespace WCF_Server
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IService
    {
        public ServerDatabase ServerDatabase { get; set; } = new ServerDatabase();
        public Dictionary<ConnectedUser, IServiceCallback> ConnectedUsersDictionary { get; set; } = new Dictionary<ConnectedUser, IServiceCallback>();
        public List<ConnectedUser> ConnectedUsers { get; set; } = new List<ConnectedUser>();
        public IServiceCallback ServiceCallback => OperationContext.Current.GetCallbackChannel<IServiceCallback>();
        public void SetMessageReadTime(int messageID, DateTime messageReadTime)
        {
            MessageContract messageToSetReadTime = ServerDatabase.MessageContracts.ToList().Find(messageContract => messageContract.MessageID == messageID);
            messageToSetReadTime.MessageReadTime = messageReadTime;
            ServerDatabase.Entry(messageToSetReadTime).State = EntityState.Modified;
            ServerDatabase.SaveChanges();
        }
        public bool ChangeUserBio(int userID, string newBio)
        {
            UserContract userToChangeBio = ServerDatabase.UserContracts.Find(userID);
            if (userToChangeBio != null && !string.IsNullOrEmpty(newBio))
            {
                userToChangeBio.Bio = newBio;
                ServerDatabase.Entry(userToChangeBio).State = EntityState.Modified;
                ServerDatabase.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeUserEmail(int userID, string newEmail)
        {
            UserContract userToChangeEmail = ServerDatabase.UserContracts.Find(userID);
            if (userToChangeEmail != null && !string.IsNullOrEmpty(newEmail) && !CheckEmailDatabaseExistence(newEmail) && IsEmailValid(newEmail))
            {
                userToChangeEmail.Email = newEmail;
                ServerDatabase.Entry(userToChangeEmail).State = EntityState.Modified;
                ServerDatabase.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeUserName(int userID, string newName)
        {
            UserContract userToChangeName = ServerDatabase.UserContracts.Find(userID);
            if (userToChangeName != null && !string.IsNullOrEmpty(newName))
            {
                userToChangeName.Name = newName;
                userToChangeName.Initials = SetInitialsFromName(newName);
                ServerDatabase.Entry(userToChangeName).State = EntityState.Modified;
                ServerDatabase.SaveChanges();

                return true;
            }
            return false;
        }

        public bool ChangeUserPassword(int userID, byte[] newPasswordHash)
        {
            UserContract userToChangePassword = ServerDatabase.UserContracts.Find(userID);
            if(userToChangePassword != null)
            {
                userToChangePassword.PasswordHash = newPasswordHash;
                ServerDatabase.Entry(userToChangePassword).State = EntityState.Modified;
                ServerDatabase.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeUserUsername(int userID, string newUsername)
        {
            UserContract userToChangeUsername = ServerDatabase.UserContracts.Find(userID);
            if (userToChangeUsername != null && !string.IsNullOrEmpty(newUsername) && !CheckUsernameDatabaseExistence(newUsername) && IsUsernameValid(newUsername))
            {
                userToChangeUsername.Username = newUsername;
                ServerDatabase.Entry(userToChangeUsername).State = EntityState.Modified;
                ServerDatabase.SaveChanges();
                return true;
            }
            return false;
        }
        public bool ChangeUserAvatar(int userID, byte[] avatarBytes, string avatarCheckSum, string avatarFileName)
        {
            UserContract userToChangeAvatar = ServerDatabase.UserContracts.Find(userID);
            if(userToChangeAvatar != null)
            {
                userToChangeAvatar.AvatarBytes = avatarBytes;
                userToChangeAvatar.AvatarCheckSum = avatarCheckSum;
                userToChangeAvatar.AvatarFileName = avatarFileName;
                ServerDatabase.Entry(userToChangeAvatar).State = EntityState.Modified;
                ServerDatabase.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CheckEmailDatabaseExistence(string email)
        {
            if (ServerDatabase.UserContracts.Count() != 0)
            {
                foreach (var user in ServerDatabase.UserContracts)
                {
                    if (user.Email == email)
                        return true;
                }
            }
            return false;
        }

        public bool CheckUsernameDatabaseExistence(string username)
        {
            if (ServerDatabase.UserContracts.Count() != 0)
            {
                foreach (var user in ServerDatabase.UserContracts)
                {
                    if (user.Username == username)
                        return true;
                }
            }
            return false;
        }

        public string Connect()
        {
            Console.WriteLine("Someone tried to connect to server!");
            return "You have connected to the server!";
        }

        public int GetNextUserID()
        {
            if (ServerDatabase.UserContracts.ToList().Count != 0)
            {            
                return ServerDatabase.UserContracts.ToList().Last().UserID + 1;
            }
            return 1;
        }
        public int GetNextChatID()
        {
            if(ServerDatabase.ChatContracts.ToList().Count != 0)
            {
                return ServerDatabase.ChatContracts.ToList().Last().ChatID + 1;
            }
            return 1;
        }
        public int GetNextMessageID()
        {
            if (ServerDatabase.MessageContracts.ToList().Count != 0)
            {
                return ServerDatabase.MessageContracts.ToList().Last().MessageID + 1;
            }
            return 1;
        }
        public UserContract GetUserByEmail(string email)
        {
            UserContract returnUser = ServerDatabase.UserContracts.ToList().Find(user => user.Email == email);
            return returnUser;
        }

        public List<UserContract> GetUserInterlocutors(int userID)
        {
            UserContract userToFindInterlocutors = new UserContract(ServerDatabase.UserContracts.ToList().Find(user => user.UserID == userID));
            List<UserContract> usersInterlocutors = new List<UserContract>();
            if(userToFindInterlocutors.Chats != null)
                 foreach(var chat in userToFindInterlocutors.Chats)
                 {
                     if (chat.UserID1 != userToFindInterlocutors.UserID)
                         usersInterlocutors.Add(ServerDatabase.UserContracts.ToList().Find(user => user.UserID == chat.UserID1));
                    if (chat.UserID2 != userToFindInterlocutors.UserID)
                        usersInterlocutors.Add(ServerDatabase.UserContracts.ToList().Find(user => user.UserID == chat.UserID2));
                 }
            return usersInterlocutors;
        }

        public List<UserContract> GetUsersByUsername(string usernameSubstring)
        {
            return new List<UserContract>(ServerDatabase.UserContracts.ToList().Where(user => user.Username.Contains(usernameSubstring)));
        }
        public bool IsServerHasChat(int userID1, int userID2)
        {
            ChatContract serverChat = ServerDatabase.ChatContracts.ToList().Find(chat => userID1 == chat.UserID1 && userID2 == chat.UserID2 || userID1 == chat.UserID2 && userID2 == chat.UserID1);
            return serverChat != null;
        }
        public bool CreateChat(int userID1, int userID2)
        {
            try
            {
                ChatContract newChatContract = new ChatContract(userID1, userID2);
                List<UserContract> userContracts = ServerDatabase.UserContracts.ToList().FindAll(userContract => userContract.UserID == userID1 || userContract.UserID == userID2);
                foreach (var userContract in userContracts)
                {
                    if (userContract.Chats == null)
                        userContract.Chats = new ObservableCollection<ChatContract>();
                    userContract.Chats.Add(newChatContract);
                }
                ServerDatabase.ChatContracts.Add(newChatContract);
                ServerDatabase.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void LoginOnServer(UserContract connectedUserContract, int userServerDatabaseID)
        {
            OperationContext OperationContext = OperationContext.Current;
            MessageProperties MessageProperties = OperationContext.IncomingMessageProperties;
            RemoteEndpointMessageProperty remoteEndpointMessageProperty = (RemoteEndpointMessageProperty)MessageProperties[RemoteEndpointMessageProperty.Name];
            ConnectedUser connectedUser = new ConnectedUser(remoteEndpointMessageProperty.Address + ":" + remoteEndpointMessageProperty.Port.ToString(), userServerDatabaseID);
            if (ConnectedUsers.Find(user => user.UserID == connectedUser.UserID) == null)
            {
                ConnectedUsers.Add(connectedUser);
                ConnectedUsersDictionary.Add(connectedUser, ServiceCallback);
            }
        }
        public void Disconnect(int userToDisconnectID)
        {
            ConnectedUsers.Remove(ConnectedUsers.Find(user => user.UserID == userToDisconnectID));
        }
        public bool Register(UserContract userToRegister)
        {
            try
            {
                ServerDatabase.UserContracts.Add(userToRegister);
                ServerDatabase.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMessage(int sentByUserID, int sentToUserID, MessageContract messageContract)
        {
            ConnectedUser userToSendMessage = ConnectedUsers.Find(user => user.UserID == sentToUserID);
            UserContract userContractToAddMessage = ServerDatabase.UserContracts.ToList().Find(userContract => userContract.UserID == sentByUserID || userContract.UserID == sentToUserID);
            if (userToSendMessage != null)
            {
                IServiceCallback serviceCallback = ConnectedUsersDictionary[userToSendMessage];
                serviceCallback.ReceiveMessage(messageContract);
                userContractToAddMessage.Chats.ToList().Find(chat => chat.ChatID == messageContract.ChatID).Messages.Add(messageContract);
                ServerDatabase.MessageContracts.Add(messageContract);
                ServerDatabase.SaveChanges();
                return true;
            }
            else
            {
                userContractToAddMessage.Chats.ToList().Find(chat => chat.ChatID == messageContract.ChatID).Messages.Add(messageContract);
                ServerDatabase.MessageContracts.Add(messageContract);
                ServerDatabase.SaveChanges();
                return true;
            }

        }
        #region Helper Methods
        public bool IsEmailValid(string email)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[a-zA-Z]{2}|aero|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel)$");
            Match match = emailRegex.Match(email);
            return match.Success;
        }
        public bool IsUsernameValid(string username)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]{4,11}$");
            Match match = emailRegex.Match(username);
            return match.Success;
        }
        public string SetInitialsFromName(string name)
        {
            string initials = string.Empty;
            name = Regex.Replace(name, @"\s+", " ");
            string[] nameWords = name.Split(' ');
            if (nameWords.Count() == 1)
            {
                initials += nameWords[0].ToCharArray()[0];
                initials += nameWords[0].ToCharArray()[0];
            }
            else
            {
                for (int i = 0; i < 2; i++)
                    initials += nameWords[i].ToCharArray()[0];
            }
            return initials.ToUpper();
        }



        #endregion
    }

}
