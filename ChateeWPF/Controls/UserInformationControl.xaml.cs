using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChateeWPF
{
    /// <summary>
    /// Interaction logic for UserInformationControl.xaml
    /// </summary>
    public partial class UserInformationControl : UserControl
    {
        public UserInformationControl()
        {
            InitializeComponent();
            DataContext = IoCContainer.Get<UserInformationViewModel>();
        }
    }
}
