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
    /* this is where the bindings are found for the ClientTab view
     */
    class ClientTabVM : ViewModelBase
    {
        #region Constructors
        public ClientTabVM() {
            Name = "Clients";
        }
        #endregion

        #region Properties

        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
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
            }
        }

        public string NewClientName
        {
            get { return NewClient.ClientCompanyName; }
            set { NewClient.ClientCompanyName = value; OnPropertyChanged("NewClientName"); }
        }
        
        public string NewClientContact
        {
            get { return NewClient.ClientContactName; }
            set { NewClient.ClientContactName = value; OnPropertyChanged("NewClientContact"); }
        }
        public string NewClientPhone
        {
            get { return NewClient.ClientContactPhone; }
            set { NewClient.ClientContactPhone = value; OnPropertyChanged("NewClientPhone"); }
        }
        public string NewClientEmail
        {
            get { return NewClient.ClientContactEMail; }
            set { NewClient.ClientContactEMail = value; OnPropertyChanged("NewClientEmail"); }
        }
        #endregion

        RelayCommand _addClientCommand;
        public ICommand AddClientCommand
        {
            get {
                if (_addClientCommand == null)
                {
                    _addClientCommand = new RelayCommand(param => this.AddClient(param));
                }
                return _addClientCommand;
            }
            
        }
        public void AddClient(object param)
        {
            // validate stuff TBD

            // save to database TBD
            // add client to list            
            _clients.Add(NewClient);
            OnPropertyChanged("Clients");

            // reset the view
            NewClient = new Client();            
            OnPropertyChanged("NewClientName");
            OnPropertyChanged("NewClientContact");
            OnPropertyChanged("NewClientPhone");
            OnPropertyChanged("NewClientEmail");
        }
    }
}
