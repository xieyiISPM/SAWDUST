using SawDust.BusinessObjects;
using SawDust.DataAccess;
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
        private IDataAccess _dao;
        #region Constructors
        public DashboardTabVM() {
            Name = "Dashboard";
            _dao = new SqliteDataAccess();
            RefreshUsers();
        }
        #endregion

        #region Properties

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged("Users"); }
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
            _dao.Add(NewUser);
            // add client to list
            _users.Add(NewUser);
            OnPropertyChanged("Users");

            // reset the view
            NewUser = new User();
            OnPropertyChanged("NewUserName");
            OnPropertyChanged("NewUserID");
            OnPropertyChanged("NewUserPassword");
            ////RefreshUsers();
        }

        RelayCommand _removeUserCommand;
        public ICommand RemoveUserCommand
        {
            get
            {
                if (_removeUserCommand == null)
                {
                    _removeUserCommand = new RelayCommand(param => this.RemoveUser(param));
                }
                return _removeUserCommand;
            }

        }
        public void RemoveUser(object param)
        {
            // validate stuff TBD
            User rUser = _users.Where(u => param.Equals(u.ID)).First();

            // save to database TBD
            _dao.Delete(rUser);
            // remove from list
            _users.Remove(rUser);
            OnPropertyChanged("Users");
            RefreshUsers();
        }

        private void RefreshUsers()
        {
            Users = new ObservableCollection<User>(_dao.getAllUsers());
        }
    }
}
