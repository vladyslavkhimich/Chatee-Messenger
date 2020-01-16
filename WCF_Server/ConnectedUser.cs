using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Server
{
    public class ConnectedUser
    {
        public int UserID { get; set; }
        public IServiceCallback ServiceCallback => OperationContext.Current.GetCallbackChannel<IServiceCallback>();
        public ConnectedUser()
        {

        }
        public ConnectedUser(int userID)
        {
            UserID = userID;
        }
    }
}
