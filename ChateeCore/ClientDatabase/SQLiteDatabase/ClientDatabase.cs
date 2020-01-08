using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class ClientDatabase : DbContext
    {
        public ClientDatabase() : base("DefaultClientDatabaseConnection")
        {

            UserContracts.Load();
            ChatContracts.Load();
            MessageContracts.Load();
            List<UserContract> userContracts = new List<UserContract>(UserContracts.ToList());
            List<ChatContract> chatContracts = new List<ChatContract>(ChatContracts.ToList());
            List<MessageContract> messageContracts = new List<MessageContract>(MessageContracts.ToList());
            //foreach (var item in UserContracts)
            //    UserContracts.Remove(item);
            //this.SaveChanges();
            SetUserData();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<ClientDatabase>(new DropCreateDatabaseIfModelChanges<ClientDatabase>());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserContract> UserContracts { get; set; }
        public DbSet<ChatContract> ChatContracts { get; set; }
        public DbSet<MessageContract> MessageContracts { get; set; }
        public void SetUserData()
        {
            if (ChatContracts.Count() != 0 && UserContracts.Count() != 0)
            {
                UserContract user = UserContracts.ToList().Last();
                user.Chats = new ObservableCollection<ChatContract>();
                List<ChatContract> chatContracts = ChatContracts.ToList().FindAll(chat => chat.UserID1 == user.UserID);
                if (chatContracts != null)
                    foreach (var chat in chatContracts)
                    {
                        user.Chats.Add(chat);
                        chat.Messages = new ObservableCollection<MessageContract>();
                        if (MessageContracts.Count() != 0)
                        {
                            List<MessageContract> messageContracts = MessageContracts.ToList().FindAll(message => chat.ChatID == message.ChatID);
                            if (messageContracts != null)
                                foreach (var message in messageContracts)
                                {
                                    if (!chat.Messages.Contains(message))
                                        chat.Messages.Add(message);
                                }
                        }
                    }
                List<ChatContract> chatContracts2 = ChatContracts.ToList().FindAll(chat => chat.UserID2 == user.UserID);
                if (chatContracts2 != null)
                    foreach (var chat in chatContracts2)
                    {
                        user.Chats.Add(chat);
                        chat.Messages = new ObservableCollection<MessageContract>();
                        if (MessageContracts.Count() != 0)
                        {
                            List<MessageContract> messageContracts = MessageContracts.ToList().FindAll(message => chat.ChatID == message.ChatID);
                            if (messageContracts != null)
                                foreach (var message in messageContracts)
                                {
                                    if (!chat.Messages.Contains(message))
                                        chat.Messages.Add(message);
                                }
                        }
                    }

            }
        }
    }
}
