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
        void LoginOnServer(UserContract connectedUser);
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
    }
    // TODO: Delete this interface if it is not needed
    public interface IServiceCallback
    {
        [OperationContract]
        void ConnectCallback(string testCallbackString);
    }
}
