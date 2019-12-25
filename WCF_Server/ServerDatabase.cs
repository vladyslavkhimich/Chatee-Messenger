using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WCF_Server.DataContracts;

namespace WCF_Server
{
    public class ServerDatabase : DbContext
    {
        public ServerDatabase() : base("DefaultConnection")
        {
            //Database.SetInitializer<ServerDatabase>(new DropCreateDatabaseIfModelChanges<ServerDatabase>());
            UserContracts.Load();
            ChatContracts.Load();
            SetUsersData();
        }
        public DbSet<UserContract> UserContracts { get; set; }
        public DbSet<ChatContract> ChatContracts { get; set; }
        #region Helper Methods
        public void SetUsersData()
        {
            if(ChatContracts.Count() != 0)
                foreach(var user in UserContracts)
                {
                    user.Chats = new ObservableCollection<ChatContract>();
                    List<ChatContract> chatContracts = ChatContracts.ToList().FindAll(chat => chat.UserID1 == user.UserID);
                    if (chatContracts != null)
                        foreach (var chat in chatContracts)
                            user.Chats.Add(chat);
                    List<ChatContract> chatContracts2 = ChatContracts.ToList().FindAll(chat => chat.UserID2 == user.UserID);
                    if (chatContracts2 != null)
                        foreach (var chat in chatContracts2)
                            user.Chats.Add(chat);
                }
        }
        #endregion
    }
}
