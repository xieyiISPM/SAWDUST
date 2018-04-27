using SawDust.BusinessObjects;
using SawDust.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace SawDust.ViewModel
{
    /* this is where the bindings are found for the ClientTab view
     */
    class UserTabVM : ViewModelBase
    {
        private IDataAccess _dao;
        #region Constructors
        public UserTabVM()
        {
            Name = "Users";
            _dao = new SqliteDataAccess();
            RefreshUsers();


        }
        #endregion

        #region Properties

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Clients
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
            set
            {
                _newUser = value;
                SubmitNewUserEnabled = ValidateForm();
            }
        }

        public string NewUserName
        {
            get { return NewUser.Name; }
            set
            {
                NewUser.Name = value; OnPropertyChanged("NewUserName");
                SubmitNewUserEnabled = ValidateForm();
            }
        }

        public string NewUserID
        {
            get { return NewUser.ID; }
            set
            {
                NewUser.ID = value; OnPropertyChanged("NewUserID");
                SubmitNewUserEnabled = ValidateForm();
            }
        }
        public string NewUserPassword
        {
            get { return NewUser.Password; }
            set
            {
                NewUser.Password = value; OnPropertyChanged("NewUserPassword");
                SubmitNewUserEnabled = ValidateForm();
            }
        }
    
    

        private bool _submitNewUserEnabled = false;
        public bool SubmitNewUserEnabled
        {
            get { return _submitNewUserEnabled; }
            set { _submitNewUserEnabled = value; OnPropertyChanged("SubmitNewUserEnabled"); }
        }

 
        #endregion



        #region remove user
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
            // remove from database TBD
            // remove client from list            
            // _clients.Remove(NewClient);
            // _dao.Delete()

        }
        #endregion

        #region add user
        RelayCommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (_addUserCommand == null)
                {
                    _addUserCommand = new RelayCommand(param => this.AddUser(param));
                }
                return _addUserCommand;
            }

        }
        public void AddUser(object param)
        {
            // save to database
            _dao.Add(NewUser);


            // reset the view
            NewUser = new User();
            OnPropertyChanged("NewUserName");
            OnPropertyChanged("NewUserID");
            OnPropertyChanged("NewUserPassword");


            RefreshUsers();
        }
        #endregion

        #region validate inputs
        public bool ValidateForm()
        {
            bool status = true;
            if (String.IsNullOrEmpty(NewUserName))
                status = false;
            if (String.IsNullOrEmpty(NewUserID))
                status = false;
            if (String.IsNullOrEmpty(NewUserPassword))
                status = false;
            return status;
        }
        #endregion
        private void RefreshUsers()
        {
            Clients = new ObservableCollection<User>(_dao.getAllUsers());
        }
    }

}
