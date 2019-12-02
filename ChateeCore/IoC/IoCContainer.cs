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
        public static void Setup()
        {
            BindViewModels();
        }

        private static void BindViewModels()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel()).InSingletonScope();
        }
    }
}
