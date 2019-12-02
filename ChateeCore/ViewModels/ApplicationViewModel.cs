using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPages CurrentPage { get; set; } = ApplicationPages.LoginPage;
        public bool IsSideMenuVisible { get; set; } = false;
    }
}
