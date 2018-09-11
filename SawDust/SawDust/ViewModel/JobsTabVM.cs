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
        public Boolean TabsVisibility { get { return true; }  }
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _clients;}
            set {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }

        private Client _selectedMainClient;
        public Client SelectedMainClient
        {
            get { return _selectedMainClient; }
            set {
                _selectedMainClient = value;
                OnPropertyChanged("SelectedMainClient");
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

        private object _selectedJob;
        public object SelectedJob
        {
            get
            {
                return _selectedJob;
            }
            set
            {
                _selectedJob = value;
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
        private string _newSalesTax;
        public string NewSalesTax { get { return _newSalesTax; }
            set {

                if (null != value)
                {
                    double tax;
                    bool parse = Double.TryParse(value ,out tax);
                    if (parse)
                    {
                        NewJob.SalesTax = tax;
                        _newSalesTax = value;
                        OnPropertyChanged("NewSalesTax");
                        SubmitEnabled = ValidateForm();
                    } else
                    {
                        _newSalesTax = null;
                        OnPropertyChanged("NewSalesTax");
                        SubmitEnabled = ValidateForm();
                    }
                   
                }
            }
        }

        private string _newDefaultHeight;
        public string NewDefaultHeight { get { return _newDefaultHeight; }
            set {
               
                if (null != value)
                {
                    double h;
                    bool parse = Double.TryParse(value, out h);
                    if (parse)
                    {
                        NewJob.DefaultHeight = h;
                        _newDefaultHeight = value;
                        OnPropertyChanged("NewDefaultHeight");
                        SubmitEnabled = ValidateForm();
                    } else
                    {
                        _newDefaultHeight = null;
                        OnPropertyChanged("NewDefaultHeight");
                        SubmitEnabled = ValidateForm();
                    }
                }
            }
        }
        private string _newMarkupPct;
        public string NewMarkupPct { get { return _newMarkupPct; }
            set {
                
                if (null != value)
                {
                    double pct;
                    bool parse = Double.TryParse(value, out pct);
                    if (parse)
                    {
                        NewJob.MarkupPct = pct;
                        _newMarkupPct = value;
                        OnPropertyChanged("NewMarkupPct");
                        SubmitEnabled = ValidateForm();
                    }
                    else
                    {
                        _newMarkupPct = null;
                        OnPropertyChanged("NewMarkupPct");
                        SubmitEnabled = ValidateForm();
                    }
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
            if (SelectedClient == null)
                status = false;
            else if (NewClientName == null)
                status = false;
            else if (NewDefaultHeight == null)
                status = false;
            else if (NewJobDescription == null)
                status = false;
            else if (NewJobName == null)
                status = false;
            else if (NewMarkupPct == null)
                status = false;
            else if (NewSalesTax == null)
                status = false;

            return status;
        }
        #endregion
        private void RefreshJobs()
        {
            Jobs = new ObservableCollection<Job>(_dao.GetAllJobs());
        }
    }
}
