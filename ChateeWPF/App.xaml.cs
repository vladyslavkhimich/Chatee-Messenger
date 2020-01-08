using ChateeCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ServiceModel;
using static WCF_Server.DataContracts;
using System.Data.Entity;
using System.IO;

namespace ChateeWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void ConnectCallback(string testCallbackString)
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IoCContainer.Setup();
            if (!TryToLoginUserWithKeepMeLoggedInTrue())
            {
                IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.LoginPage);
            }
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
        public bool TryToLoginUserWithKeepMeLoggedInTrue()
        {
            if (IoCContainer.Get<ApplicationViewModel>().ClientDatabase.UserContracts.ToList().Count != 0)
            {
                var userClientDatabase = IoCContainer.Get<ApplicationViewModel>().ClientDatabase.UserContracts.ToList().Last();
                if (userClientDatabase.IsKeepLoggedIn && IoCContainer.Get<ApplicationViewModel>().IsServerReachable)
                {
                    var userServerDatabase = IoCContainer.Get<ApplicationViewModel>().ServiceClient.GetUserByEmail(userClientDatabase.Email);
                    if (!LoginHelper.IsServerAndClientUserSame(userClientDatabase, userServerDatabase))
                    { 
                        LoginHelper.ChangeClientDatabaseUser(userServerDatabase, true);
                        userClientDatabase = IoCContainer.Get<ApplicationViewModel>().ClientDatabase.UserContracts.ToList().Last();
                    }
                    if (!string.Equals(userServerDatabase.AvatarCheckSum, userClientDatabase.AvatarCheckSum))
                    {
                        if (!LoginHelper.IsClientDatabaseHasAvatar(userServerDatabase.AvatarCheckSum))
                            Task.Run(() => LoginHelper.SetUserAvatar(userServerDatabase.UserID, userServerDatabase.AvatarCheckSum, userServerDatabase.AvatarFileName, userServerDatabase.AvatarBytes));
                        else
                        {
                            Task<string> searchingAvatarTask = Task.Run(() => FileHelper.GetFilePathWithCheckSum(userServerDatabase.AvatarCheckSum, "/ClientDatabase/Database/ImageDatabase/Avatars/"));
                            string avatarPath = searchingAvatarTask.Result;
                            Task.Run(() => LoginHelper.SetUserAvatar(userServerDatabase.UserID, FileHelper.ComputeFileCheckSum(avatarPath), new FileInfo(avatarPath).Name));
                        }
                    }
                    LoginHelper.SetApplicationUser(userClientDatabase, userClientDatabase.ServerDatabaseUserID);
                    LoginHelper.SetInterlocutorsList();
                    LoginHelper.SetUsersChatMessageLists();
                    IoCContainer.Get<SettingsViewModel>().SetUsersData();
                    IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage);
                    return true;
                }
                else if (!IoCContainer.Get<ApplicationViewModel>().IsServerReachable)
                {
                    
                    return false;
                }
            }
            return false;
        }
        
    }
}
