using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public static class LoginHelper
    {
        public static ApplicationViewModel ApplicationViewModel => IoCContainer.Get<ApplicationViewModel>();
        public static bool IsServerAndClientUserSame(UserContract clientUser, UserContract serverUser)
        {
            if (clientUser.Chats == null && serverUser.Chats != null || clientUser.Chats != null && serverUser.Chats == null)
                return false;
            if (serverUser.Chats != null && clientUser.Chats != null)
                if (serverUser.Chats.Count != clientUser.Chats.Count)
                    return false;
            if (serverUser.Name != clientUser.Name)
                return false;
            if (serverUser.Email != clientUser.Email)
                return false;
            if (serverUser.Username != clientUser.Username)
                return false;
            if (serverUser.Bio != clientUser.Bio)
                return false;
            if (serverUser.PasswordHash != clientUser.PasswordHash)
                return false;
            if (serverUser.AvatarCheckSum != clientUser.AvatarCheckSum)
                return false;
            for (int i = 0; i < clientUser.Chats.Count; i++)
            {
                if (clientUser.Chats[i].Messages.Count != serverUser.Chats[i].Messages.Count)
                    return false;
            }
            return true;
        }
        public static void ChangeClientDatabaseUser(UserContract serverUser, bool isKeepLoggedIn)
        {
            if (ApplicationViewModel.ClientDatabase.UserContracts.ToList().Count != 0)
            {
                if (ApplicationViewModel.ClientDatabase.UserContracts.ToList().Last() != null)
                {
                    ApplicationViewModel.ClientDatabase.UserContracts.Remove(IoCContainer.Get<ApplicationViewModel>().ClientDatabase.UserContracts.ToList().Last());
                    ApplicationViewModel.ClientDatabase.Entry(IoCContainer.Get<ApplicationViewModel>().ClientDatabase.UserContracts.ToList().Last()).State = EntityState.Deleted;
                    ApplicationViewModel.ClientDatabase.SaveChanges();
                }
            }
            ApplicationViewModel.ClientDatabase.UserContracts.Add(new UserContract(serverUser, isKeepLoggedIn));
            ApplicationViewModel.ClientDatabase.SaveChanges();
        }
        public static void RemoveClientDatabaseUser(UserContract userToRemove)
        {
            ApplicationViewModel.ClientDatabase.UserContracts.Remove(userToRemove);
            ApplicationViewModel.ClientDatabase.Entry(userToRemove).State = EntityState.Deleted;
            ApplicationViewModel.ClientDatabase.SaveChanges();
        }
        public static void SetApplicationUser(WCF_Server.DataContracts.UserContract loggedUser, int serverDatabaseUserID)
        {
            ApplicationViewModel.CurrentUser = new User(loggedUser);
            ApplicationViewModel.CurrentUserContract = loggedUser;
            ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID = serverDatabaseUserID;
        }
        public static void SetUsersAndFilesLists()
        {
            ApplicationViewModel.UserInterlocutors = new ObservableCollection<UserContract>(IoCContainer.Get<ApplicationViewModel>().ServiceClient.GetUserInterlocutors(IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.ServerDatabaseUserID).ToList());
            IoCContainer.Get<UserListViewModel>().SetUsersFromDatabase(IoCContainer.Get<ApplicationViewModel>().UserInterlocutors.ToList());
        }
        public static void SwitchToChatPage()
        {
            IoCContainer.Get<SettingsViewModel>().SetUsersData();
            ApplicationViewModel.GoToPage(ApplicationPages.ChatPage);
            ApplicationViewModel.IsSideMenuVisible = true;
        }
        public static bool IsClientDatabaseHasAvatar(string avatarCheckSum)
        {
            string[] avatarPathes = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/ClientDatabase/Database/ImageDatabase/Avatars/");
            foreach (var avatarPath in avatarPathes)
                if (avatarCheckSum == FileHelper.ComputeFileCheckSum(avatarPath))
                    return true;
            return false;
        }
        
        public static void SetUserAvatar(int userID, string avatarCheckSum, string avatarFileName, byte[] avatarBytes = null)
        {
            var userToChangeAvatar = ApplicationViewModel.ClientDatabase.UserContracts.ToList().Find(user => user.ServerDatabaseUserID == userID);
            if (avatarBytes != null)
            {
                ApplicationViewModel.ServiceClient.ChangeUserAvatar(userID, avatarBytes, avatarCheckSum, avatarFileName);
                userToChangeAvatar.AvatarCheckSum = avatarCheckSum;
                userToChangeAvatar.AvatarFileName = avatarFileName;
                userToChangeAvatar.AvatarBytes = avatarBytes;
                ApplicationViewModel.ClientDatabase.Entry(userToChangeAvatar).State = EntityState.Modified;
                ApplicationViewModel.ClientDatabase.SaveChanges();
                FileHelper.ConvertArrayOfBytesToAvatar(avatarFileName, avatarBytes);
                IoCContainer.Get<SettingsViewModel>().UserProfileImagePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/ClientDatabase/Database/ImageDatabase/Avatars/" + avatarFileName;
            }
            else
            {
                userToChangeAvatar.AvatarCheckSum = avatarCheckSum;
                userToChangeAvatar.AvatarFileName = avatarFileName;
                ApplicationViewModel.ClientDatabase.Entry(userToChangeAvatar).State = EntityState.Modified;
                ApplicationViewModel.ClientDatabase.SaveChanges();
                IoCContainer.Get<SettingsViewModel>().UserProfileImagePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/ClientDatabase/Database/ImageDatabase/Avatars/" + avatarFileName;
            }
        }
    }
}
