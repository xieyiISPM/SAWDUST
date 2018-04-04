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
    /* this is where the bindings are found for the DashboardTab view
     */
    class DashboardTabVM : ViewModelBase
    {
        #region Constructors
        public DashboardTabVM() {
            Name = "Dashboard";
            _users.Add(new User());

            _users.Add(new User());
            _users.First().Name = "Ad Min";
            _users.First().Password = "password";
            _users.First().ID = "admin@gmail.com";
        }
        #endregion

        #region Properties

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get { return _users; }
        }
        private User _newUser;
        public User NewUser
        {
            get
            {
                if (null == _newUser)
                    _newUser = new User();
                return _newUser;
            }
            set {
                _newUser = value;
            }
        }

        public string NewUserName
        {
            get { return NewUser.Name; }
            set { NewUser.Name = value; OnPropertyChanged("NewUserName"); }
        }
        
        public string NewUserID
        {
            get { return NewUser.ID; }
            set { NewUser.ID = value; OnPropertyChanged("NewUserID"); }
        }
        public string NewClientPhone
        {
            get { return NewUser.Password; }
            set { NewUser.Password = value; OnPropertyChanged("NewUserPassword"); }
        }
        #endregion

        RelayCommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get {
                if (_addUserCommand == null)
                {
                    _addUserCommand = new RelayCommand(param => this.AddUser(param));
                }
                return _addUserCommand;
            }
            
        }
        public void AddUser(object param)
        {
            // validate stuff TBD

            // save to database TBD
            // add client to list
            _users.Add(NewUser);
            OnPropertyChanged("Users");

            // reset the view
            NewUser = new User();            
            OnPropertyChanged("NewUserName");
            OnPropertyChanged("NewUserID");
            OnPropertyChanged("NewUserPassword");
        }
    }
}
