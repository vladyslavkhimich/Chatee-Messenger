using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Server
{
    public class ConnectedUser
    {
        public string IPAddress { get; set; }
        public int UserID { get; set; }
        public ConnectedUser()
        {

        }
        public ConnectedUser(string ipAddress, int userID)
        {
            IPAddress = ipAddress;
            UserID = userID;
        }
    }
}
