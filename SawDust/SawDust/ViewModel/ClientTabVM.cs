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
    class ClientTabVM : ViewModelBase
    {
        private IDataAccess _dao;
        #region Constructors
        public ClientTabVM() {
            Name = "Clients";
            _dao = new SqliteDataAccess();
            RefreshClients();

            Statuses.Add(new ComboBoxItem() { ComboBoxOption = "Active" });
            Statuses.Add(new ComboBoxItem() { ComboBoxOption = "Inactive" });

            SelectedStateOption = Statuses.First();

        }
        #endregion

        #region Properties

        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; OnPropertyChanged("Clients"); }
        }
        private ObservableCollection<ComboBoxItem> _statuses = new ObservableCollection<ComboBoxItem>();
        public ObservableCollection<ComboBoxItem> Statuses
        {
            get { return _statuses; }
        }

        private Client _newClient;
        public Client NewClient{
            get
            {
                if (null == _newClient)
                    _newClient = new Client();
                return _newClient;
            }
            set {
                _newClient = value;
                SubmitNewClientEnabled = ValidateForm();
            }
        }

        public string NewClientName
        {
            get { return NewClient.ClientCompanyName; }
            set {
                NewClient.ClientCompanyName = value; OnPropertyChanged("NewClientName");
                SubmitNewClientEnabled = ValidateForm();
            }
        }
        
        public string NewClientContact
        {
            get { return NewClient.ClientContactName; }
            set {
                NewClient.ClientContactName = value; OnPropertyChanged("NewClientContact");
                SubmitNewClientEnabled = ValidateForm();
            }
        }
        public string NewClientPhone
        {
            get { return NewClient.ClientContactPhone; }
            set {
                NewClient.ClientContactPhone = value; OnPropertyChanged("NewClientPhone");
                SubmitNewClientEnabled = ValidateForm();
            }
        }
        public string NewClientEmail
        {
            get { return NewClient.ClientContactEMail; }
            set {
                NewClient.ClientContactEMail = value; OnPropertyChanged("NewClientEmail");
                SubmitNewClientEnabled = ValidateForm();
            }
        }
        public string NewClientStatus
        {
            get
            {
                return NewClient.Status;
            }
            set
            {
                NewClient.Status = value; OnPropertyChanged("NewClientStatus");
                SubmitNewClientEnabled = ValidateForm();
            }
        }

        private bool _submitNewClientEnabled = false;
        public bool SubmitNewClientEnabled
        {
            get { return _submitNewClientEnabled; }
            set { _submitNewClientEnabled = value; OnPropertyChanged("SubmitNewClientEnabled"); }
        }

        private ComboBoxItem _selectedStateOption;
        public ComboBoxItem SelectedStateOption
        {
            get { return _selectedStateOption; }
            set
            {
                _selectedStateOption = value;
                NewClientStatus = value.ComboBoxOption;
                OnPropertyChanged("SelectedStateOption");
            }
        }
        #endregion



        #region remove client
        RelayCommand _removeClientCommand;
        public ICommand RemoveClientCommand
        {
            get
            {
                if (_removeClientCommand == null)
                {
                    _removeClientCommand = new RelayCommand(param => this.RemoveClient(param));
                }
                return _removeClientCommand;
            }
        }
        public void RemoveClient(object param)
        {
            // validate stuff TBD
            // remove from database TBD
            // remove client from list            
            // _clients.Remove(NewClient);
            // _dao.Delete()

        }
        #endregion

        #region add client
        RelayCommand _addClientCommand;
        public ICommand AddClientCommand
        {
            get
            {
                if (_addClientCommand == null)
                {
                    _addClientCommand = new RelayCommand(param => this.AddClient(param));
                }
                return _addClientCommand;
            }

        }
        public void AddClient(object param)
        {
            // save to database
            _dao.Add(NewClient);

            
            // reset the view
            NewClient = new Client();
            OnPropertyChanged("NewClientName");
            OnPropertyChanged("NewClientContact");
            OnPropertyChanged("NewClientPhone");
            OnPropertyChanged("NewClientEmail");
            OnPropertyChanged("NewClientStatus");

            SelectedStateOption = Statuses.First();
            RefreshClients();
        }
        #endregion

        #region validate inputs
        public bool ValidateForm()
        {
            bool status = true;
            if (String.IsNullOrEmpty(NewClientContact))
                status = false;
            if (String.IsNullOrEmpty(NewClientEmail))
                status = false;
            if (String.IsNullOrEmpty(NewClientName))
                status = false;
            if (String.IsNullOrEmpty(NewClientPhone))
                status = false;
            if (String.IsNullOrEmpty(NewClientStatus))
                status = false;
            return status;
        }
        #endregion
        private void RefreshClients()
        {
            Clients = new ObservableCollection<Client>(_dao.GetAllClients());
        }
    }
    public class ComboBoxItem
    {
        public string ComboBoxOption { get; set; }
    }
}
