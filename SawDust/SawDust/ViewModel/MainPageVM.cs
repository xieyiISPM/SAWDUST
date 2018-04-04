using SawDust.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SawDust.ViewModel
{
    /* this is where the bindings are found for the MainPage view
     */
    class MainPageVM : ViewModelBase
    {
        #region Properties
        public string TabsVisibility { get; set; }
        public string SignInVisibility { get; set; }
        public string BadCredsVisibility { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors
        public MainPageVM() {
            Name = "MainPage";
            TabsVisibility = "Collapsed";
            BadCredsVisibility = "Collapsed";
            SignInVisibility = "Visible";
            Email = "";
            Password = "";
        }
        #endregion

        RelayCommand _signIn;
        public ICommand SignInCommand
        {
            get {
                if (_signIn == null)
                {
                    _signIn = new RelayCommand(param => this.SignIn(param));
                }
                return _signIn;
            }
        }
        public void SignIn(object param)
        {
            SignInVisibility = "Collapsed";
            OnPropertyChanged("SignInVisibility");
            if (Email == "admin@gmail.com" && Password == "password") {
                TabsVisibility = "Visible";
                OnPropertyChanged("TabsVisibility");
            } else {
                BadCredsVisibility = "Visible";
                OnPropertyChanged("BadCredsVisibility");
            }
        }

        RelayCommand _retryLogin;
        public ICommand RetryLoginCommand
        {
            get
            {
                if (_retryLogin == null)
                {
                    _retryLogin = new RelayCommand(param => this.RetryLogin(param));
                }
                return _retryLogin;
            }
        }
        public void RetryLogin(object param)
        {
            SignInVisibility = "Visible";
            OnPropertyChanged("SignInVisibility");
            TabsVisibility = "Collapsed";
            OnPropertyChanged("TabsVisibility");
            BadCredsVisibility = "Collapsed";
            OnPropertyChanged("BadCredsVisibility");
        }
    }
}
