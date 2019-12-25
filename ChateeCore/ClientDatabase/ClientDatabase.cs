using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ClientDatabase : DbContext
    {
        public ClientDatabase() : base("DefaultConnection")
        {
            UserContracts.Load();
        }
        public DbSet<WCF_Server.DataContracts.UserContract> UserContracts { get; set; }
    }
}
