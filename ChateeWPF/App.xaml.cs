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
            
            List<FileAttachment> filesFromDatabase = new List<FileAttachment>
        {
            new FileAttachment("D:/Test/TestFiles/OLX Skis.docx", "D:/Test/TestFiles/OLX Skis.docx", 1),
            //new FileAttachment("D:/Test/TestFiles/GTA Vice City Flash FM Complete Track (128 kbit s) (via Skyload).mp3", "D:/Test/TestFiles/GTA Vice City Flash FM Complete Track (128 kbit s) (via Skyload).mp3", 1),
            new FileAttachment("D:/Test/TestFiles/Panama - The Highs.mp4", "D:/Test/TestFiles/Panama - The Highs.mp4", 1),
            new FileAttachment("D:/Test/TestFiles/Лабораторна робота 17 — копия.pdf", "D:/Test/TestFiles/Лабораторна робота 17 — копия.pdf", 1),
            new FileAttachment("D:/Test/TestFiles/LogoGray.psd", "D:/Test/TestFiles/LogoGray.psd", 1),
            new FileAttachment("D:/Test/TestFiles/Text for birthday.docx", "D:/Test/TestFiles/Text for birthday.docx", 1),
            new FileAttachment("D:/Test/TestFiles/OLX books description.docx", "D:/Test/TestFiles/OLX books description.docx", 1)
        };
            List<User> usersFromDatabase = new List<User>
            {
                new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."),
                new User(1, "Miha", "mihanya228@gmail.com", "Misha", "Wordpress is power", "MS", "EF1266", "Lorem ipsum dor color iotred locusto."),
                new User(1, "Violent", "himich01092001@gmail.com", "Vladyslav", "Fuck da maximalism", "VK", "889F66", "Lorem ipsum dor color iotred locusto."),
                new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."),
                new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."),
                new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."),
                new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."),
        };
            IoCContainer.Setup(filesFromDatabase);
            IoCContainer.Get<UserListViewModel>().UsersFromDatabase = new ObservableCollection<User>(usersFromDatabase);
            if(!TryToLoginUserWithKeepMeLoggedInTrue())
            IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.LoginPage);
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
                    LoginHelper.SetUsersAndFilesLists();
                    LoginHelper.SetUsersChatMessageLists();
                    LoginHelper.SwitchToChatPage();
                    return true;
                }
                else if (userClientDatabase.IsKeepLoggedIn && !IoCContainer.Get<ApplicationViewModel>().IsServerReachable)
                {
                    LoginHelper.SetApplicationUser(userClientDatabase, userClientDatabase.ServerDatabaseUserID);
                    LoginHelper.SetUsersChatMessageLists();
                    LoginHelper.SwitchToChatPage();
                    return true;
                }
            }
            return false;
        }
        
    }
}
