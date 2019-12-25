using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public static class IoCContainer
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
        public static void Setup(List<FileAttachment> filesFromDatabase)
        {
            BindViewModels(filesFromDatabase);
        }

        private static void BindViewModels(List<FileAttachment> filesFromDatabase)
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel()).InSingletonScope();
            Kernel.Bind<SettingsViewModel>().ToConstant(new SettingsViewModel()).InSingletonScope();
            Kernel.Bind<UserInformationViewModel>().ToConstant(new UserInformationViewModel()).InSingletonScope();
            Kernel.Bind<ChatListViewModel>().ToConstant(new ChatListViewModel()).InSingletonScope();
            Kernel.Bind<FileListViewModel>().ToConstant(new FileListViewModel(filesFromDatabase)).InSingletonScope();
            Kernel.Bind<UserListViewModel>().ToConstant(new UserListViewModel()).InSingletonScope();
        }
    }
}
