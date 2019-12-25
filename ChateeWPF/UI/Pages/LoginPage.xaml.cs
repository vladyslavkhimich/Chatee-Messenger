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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginPageViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegisterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((dynamic)this.DataContext).RegisterPassword = ((PasswordBox)sender).SecurePassword;
            if (((PasswordBox)sender).SecurePassword.Length < 6)
            {
                ((dynamic)this.DataContext).IsRegisterPasswordHasError = true;
                ((dynamic)this.DataContext).RegisterPasswordErrorToolTip = "Password must contain at least 6 characters";
            }
            else
                ((dynamic)this.DataContext).IsRegisterPasswordHasError = false;
        }
        private void RepeatRegisterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((dynamic)this.DataContext).RepeatRegisterPassword = ((PasswordBox)sender).SecurePassword;
            if (((PasswordBox)sender).SecurePassword.Length < 6)
            {
                ((dynamic)this.DataContext).IsRepeatRegisterPasswordHasError = true;
                ((dynamic)this.DataContext).RepeatRegisterPasswordErrorToolTip = "Password must contain at least 6 characters";
            }
            else
                ((dynamic)this.DataContext).IsRepeatRegisterPasswordHasError = false;
        }

        private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((dynamic)this.DataContext).LoginPassword = ((PasswordBox)sender).SecurePassword;
            if (((PasswordBox)sender).SecurePassword.Length < 6)
            {
                ((dynamic)this.DataContext).IsLoginPasswordHasError = true;
                ((dynamic)this.DataContext).LoginPasswordErrorToolTip = "Password must contain at least 6 characters";
            }
            else
                ((dynamic)this.DataContext).IsLoginPasswordHasError = false;
        }
    }
}
