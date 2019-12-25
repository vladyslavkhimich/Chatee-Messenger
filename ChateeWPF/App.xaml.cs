using ChateeCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ChateeWPF.ServiceReference;
using System.ServiceModel;

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
            new FileAttachment("D:/Test/TestFiles/GTA Vice City Flash FM Complete Track (128 kbit s) (via Skyload).mp3", "D:/Test/TestFiles/GTA Vice City Flash FM Complete Track (128 kbit s) (via Skyload).mp3", 1),
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
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
