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
        public ClientDatabase() : base("DefaultClientDatabaseConnection")
        {
            
            UserContracts.Load();
            ChatContracts.Load();
            MessageContracts.Load();
            List<WCF_Server.DataContracts.UserContract> userContracts = new List<WCF_Server.DataContracts.UserContract>(UserContracts.ToList());
            //foreach (var item in UserContracts)
            //    UserContracts.Remove(item);
            //this.SaveChanges();

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<ClientDatabase>(new DropCreateDatabaseAlways<ClientDatabase>());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<WCF_Server.DataContracts.UserContract> UserContracts { get; set; }
        public DbSet<WCF_Server.DataContracts.ChatContract> ChatContracts { get; set; }
        public DbSet<WCF_Server.DataContracts.MessageContract> MessageContracts { get; set; }
    }
}
