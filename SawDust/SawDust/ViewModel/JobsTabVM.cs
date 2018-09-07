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
    class JobsTabVM : ViewModelBase
    {
        private IDataAccess _dao;
        #region Constructors
        public JobsTabVM() {
            Name = "Jobs";
            _dao = new SqliteDataAccess();
            RefreshJobs();
            RefreshClients();
        }

        private void RefreshClients()
        {
            Clients = new ObservableCollection<Client>(_dao.GetAllClients());
        }
        #endregion

        #region Properties

        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _clients;}
            set {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }
        private ObservableCollection<Job> _jobs = new ObservableCollection<Job>();
        public ObservableCollection<Job> Jobs
        {
            get { return _jobs; }
            set { _jobs = value; OnPropertyChanged("Jobs"); }
        }
        private ObservableCollection<ComboBoxItem> _statuses = new ObservableCollection<ComboBoxItem>();
       
        private Job _newJob;
        public Job NewJob{
            get
            {
                if (null == _newJob)
                    _newJob = new Job();
                return _newJob;
            }
            set {
                _newJob = value;
                SubmitEnabled = ValidateForm();
            }
        }

        
        private bool _submitEnabled = false;
        public bool SubmitEnabled
        {
            get { return _submitEnabled; }
            set { _submitEnabled = value; OnPropertyChanged("SubmitEnabled"); }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value; OnPropertyChanged("SelectedClient");
                if (null != value)
                {
                    NewJob.ClientId = _selectedClient.ID;
                    NewJob.ClientName = _selectedClient.ClientCompanyName;
                }
            }
        }
        public string NewClientName
        {
            get { return NewJob.ClientName; }
            set
            {
                NewJob.ClientName = value; OnPropertyChanged("NewClientName");
            }
        }

        public string NewJobName { get { return NewJob.JobName; }
            set { NewJob.JobName = value; OnPropertyChanged("NewJobName");
                SubmitEnabled = ValidateForm();
            }
        }

        public string NewJobDescription { get { return NewJob.JobDescription; }
            set { NewJob.JobDescription = value; OnPropertyChanged("NewJobDescription");
                SubmitEnabled = ValidateForm();
            }
        }
        private Double? _newSalesTax;
        public Double? NewSalesTax { get { return _newSalesTax; }
            set {
                _newSalesTax = value;
                OnPropertyChanged("NewSalesTax");

                if (null != value)
                {
                    NewJob.SalesTax = value.Value;
                    SubmitEnabled = ValidateForm();
                }
            }
        }

        private Double? _newDefaultHeight;
        public Double? NewDefaultHeight { get { return _newDefaultHeight; }
            set {
                _newDefaultHeight = value;
                OnPropertyChanged("NewDefaultHeight");
                if (null != value)
                {
                    NewJob.DefaultHeight = value.Value; 
                    SubmitEnabled = ValidateForm();
                }
            }
        }
        private Double? _newMarkupPct;
        public Double? NewMarkupPct { get { return _newMarkupPct; }
            set {
                _newMarkupPct = value;
                OnPropertyChanged("NewMarkupPct");
                if (null != value)
                {
                    NewJob.MarkupPct = value.Value; 
                    SubmitEnabled = ValidateForm();
                }               
            }
        }



        #endregion



        #region remove job
        RelayCommand _removeJobCommand;
        public ICommand RemoveJobCommand
        {
            get
            {
                if (_removeJobCommand == null)
                {
                    _removeJobCommand = new RelayCommand(param => this.RemoveJob(param));
                }
                return _removeJobCommand;
            }
        }
        public void RemoveJob(object param)
        {
            Job j = (Job)param;
            _dao.Delete(j);
            RefreshJobs();
        }
        #endregion

        #region add job
        RelayCommand _addJobCommand;
        public ICommand AddJobCommand
        {
            get
            {
                if (_addJobCommand == null)
                {
                    _addJobCommand = new RelayCommand(param => this.AddJob(param));
                }
                return _addJobCommand;
            }

        }
        public void AddJob(object param)
        {
            // save to database
            _dao.Add(NewJob);


            // reset the form
            NewJob = new Job();
            SelectedClient = null;
            NewClientName = null;
            NewDefaultHeight = null;
            NewJobDescription = null;
            NewJobName = null;
            NewMarkupPct = null;
            NewSalesTax = null;
           
            
            RefreshJobs();
        }
        #endregion

        #region validate inputs
        public bool ValidateForm() { 
        
            
            bool status = true;
           
            return status;
        }
        #endregion
        private void RefreshJobs()
        {
            Jobs = new ObservableCollection<Job>(_dao.GetAllJobs());
        }
    }
}
