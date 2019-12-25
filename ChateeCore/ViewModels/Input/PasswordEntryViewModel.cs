using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChateeCore
{
    public class PasswordEntryViewModel : BaseViewModel
    {
        #region Public Properties
        public string Label { get; set; }
        public string FakePassword { get; set; }
        public string CurrentPasswordHintText { get; set; }
        public string NewPasswordHintText { get; set; }
        public string ConfirmPasswordHintText { get; set; }
        public SecureString CurrentPassword { get; set; }
        public SecureString NewPassword { get; set; }
        public SecureString ConfirmPassword { get; set; }
        public bool IsEditing { get; set; }
        #endregion
        #region Public Commands
        public ICommand EditCommand { get; set; } 
        public ICommand CancelCommand { get; set; } 
        public ICommand SaveCommand { get; set; } 
        #endregion
        #region Constructors
        public PasswordEntryViewModel()
        {
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
            FakePassword = "********";
            CurrentPasswordHintText = "Current Password";
            NewPasswordHintText = "New Password";
            ConfirmPasswordHintText = "Confirm Password";
        }
        #endregion
        #region Commands Methods
        public void Edit()
        {
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();
            IsEditing = true;
        }
        public void Cancel()
        {
            IsEditing = false;
        }
        public void Save()
        {
            var storedPassword = "Testing";
            if (storedPassword != CurrentPassword.Unsecure())
            {
                MessageBox.Show("The current password is invalid", "Wrong Password", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (NewPassword.Unsecure() != ConfirmPassword.Unsecure())
            {
                MessageBox.Show("The new and confirmed password do not match", "Password mismatch", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (NewPassword.Unsecure().Length == 0)
            {
                MessageBox.Show("You must enter a password", "Password is too short", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CurrentPassword = new SecureString();
            foreach (var c in NewPassword.Unsecure().ToCharArray())
            {
                CurrentPassword.AppendChar(c);
            }
            IsEditing = false;
        }
        #endregion
    }
}
