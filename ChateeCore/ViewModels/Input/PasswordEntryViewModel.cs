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
        public string CurrentPasswordErrorToolTip { get; set; }
        public string ConfirmPasswordErrorToolTip { get; set; }
        public SecureString CurrentPassword { get; set; }
        public SecureString NewPassword { get; set; }
        public SecureString ConfirmPassword { get; set; }
        public bool IsEditing { get; set; }
        public bool IsWorking { get; set; }
        public bool IsCurrentPasswordHasError { get; set; } = false;
        public bool IsConfirmPasswordHasError { get; set; } = false;
        public Func<Task<bool>> CommitAction { get; set; }
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
                bool result = true;
                RunCommandAsync(() => IsWorking, async () =>
                {
                    IsEditing = false;
                    if (!IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.PasswordHash.SequenceEqual(SecureStringHelpers.SecureStringToHash(CurrentPassword, IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.ServerDatabaseUserID)))
                    {
                        IsCurrentPasswordHasError = true;
                        CurrentPasswordErrorToolTip = "Password does not match existing password";
                        IsEditing = true;
                        return;
                    }
                    else if(ConfirmPassword.Length < 6 || NewPassword.Length < 6)
                    {
                        IsCurrentPasswordHasError = true;
                        CurrentPasswordErrorToolTip = "Password must be at least 6 characters";
                        IsEditing = true;
                        return;
                    }
                    else if(ConfirmPassword.Unsecure() != NewPassword.Unsecure())
                    {
                        IsConfirmPasswordHasError = true;
                        ConfirmPasswordErrorToolTip = "Passwords are not equal";
                        IsEditing = true;
                        return;
                    }
                    result = await CommitAction();
                }).ContinueWith(t =>
                {
                    if (!result)
                    {
                        IsEditing = true;
                    }
                });
            IsEditing = false;
        }
        #endregion
    }
}
