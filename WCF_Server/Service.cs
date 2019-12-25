using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using static WCF_Server.DataContracts;

namespace WCF_Server
{
 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        public ServerDatabase ServerDatabase { get; set; } = new ServerDatabase();
        public List<ConnectedUser> ConnectedUsers { get; set; } = new List<ConnectedUser>();
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
            if(ServerDatabase.UserContracts.ToList().Count != 0)
            return ServerDatabase.UserContracts.ToList().Last().UserID + 1;
            return 1;
        }

        public UserContract GetUserByEmail(string email)
        {
            return ServerDatabase.UserContracts.ToList().Find(user => user.Email == email);
        }

        public List<UserContract> GetUserInterlocutors(int userID)
        {
            UserContract userToFindInterlocutors = ServerDatabase.UserContracts.ToList().Find(user => user.UserID == userID);
            List<UserContract> usersInterlocutors = new List<UserContract>();
            if(userToFindInterlocutors.Chats != null)
                 foreach(var chat in userToFindInterlocutors.Chats)
                 {
                     if (chat.UserID1 != userToFindInterlocutors.UserID)
                         usersInterlocutors.Add(ServerDatabase.UserContracts.ToList().Find(user => user.UserID == chat.UserID1));
                     usersInterlocutors.Add(ServerDatabase.UserContracts.ToList().Find(user => user.UserID == chat.UserID2));
                 }
            return usersInterlocutors;
        }

        public List<UserContract> GetUsersByUsername(string usernameSubstring)
        {
            return new List<UserContract>(ServerDatabase.UserContracts.ToList().Where(user => user.Username.Contains(usernameSubstring)));
        }

        public void LoginOnServer(UserContract connectedUserContract)
        {
            OperationContext OperationContext = OperationContext.Current;
            MessageProperties MessageProperties = OperationContext.IncomingMessageProperties;
            RemoteEndpointMessageProperty remoteEndpointMessageProperty = (RemoteEndpointMessageProperty)MessageProperties[RemoteEndpointMessageProperty.Name];
            ConnectedUser connectedUser = new ConnectedUser(remoteEndpointMessageProperty.Address + ":" + remoteEndpointMessageProperty.Port.ToString(), connectedUserContract.UserID);
            ConnectedUser alreadyConnectedUser = ConnectedUsers.Find(user => user.UserID == connectedUser.UserID);
            if (alreadyConnectedUser != null && alreadyConnectedUser.IPAddress != connectedUser.IPAddress)
            {
                ConnectedUsers.Remove(alreadyConnectedUser);
                ConnectedUsers.Add(connectedUser);
                return;
            }
            else if(alreadyConnectedUser == null)
                ConnectedUsers.Add(connectedUser);
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
    }
}
