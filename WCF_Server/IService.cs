using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static WCF_Server.DataContracts;

namespace WCF_Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IService
    {
        [OperationContract]
        string Connect();
        [OperationContract]
        void LoginOnServer(UserContract connectedUser, int userServerDatabaseID);
        [OperationContract]
        void Disconnect(int userID);
        [OperationContract]
        bool Register(UserContract userToRegister);
        [OperationContract]
        UserContract GetUserByEmail(string email);
        [OperationContract]
        List<UserContract> GetUserInterlocutors(int userID);
        [OperationContract]
        List<UserContract> GetUsersByUsername(string usernameSubstring);
        [OperationContract]
        bool CheckEmailDatabaseExistence(string email);
        [OperationContract]
        bool CheckUsernameDatabaseExistence(string username);
        [OperationContract]
        int GetNextUserID();
        [OperationContract]
        int GetNextChatID();
        [OperationContract]
        int GetNextMessageID();
        [OperationContract]
        void SetMessageReadTime(int messageID, DateTime messageReadTime);
        [OperationContract]
        bool ChangeUserName(int userID, string name);
        [OperationContract]
        bool ChangeUserEmail(int userID, string email);
        [OperationContract]
        bool ChangeUserPassword(int userID, byte[] passwordHash);
        [OperationContract]
        bool ChangeUserUsername(int userID, string username);
        [OperationContract]
        bool ChangeUserBio(int userID, string bio);
        [OperationContract]
        bool ChangeUserAvatar(int userID, byte[] avatar, string avatarCheckSum, string avatarName);
        [OperationContract]
        bool SendMessage(int sentByUserID, int sentToUserID, MessageContract messageContract = null);
        [OperationContract]
        bool IsServerHasChat(int userID1, int userID2);
        [OperationContract]
        bool CreateChat(int userID1, int userID2);
    }
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(MessageContract Message);
    }
}
