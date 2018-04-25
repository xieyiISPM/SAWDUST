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
    class JobTabVM : ViewModelBase
    {

        #region Constructors
        public JobTabVM()
        {
            Name = "Jobs";
        }
        #endregion

        #region Properties

        private ObservableCollection<Job> _jobs = new ObservableCollection<Job>();
        public ObservableCollection<Job> Jobs
        {
            get { return _jobs; }
        }
        private Job _newJob;
        public Job NewJob
        {
            get
            {
                if (null == _newJob)
                    _newJob = new Job();
                return _newJob;
            }
            set
            {
                _newJob = value;
            }
        }

        public string NewJobName
        {
            get { return NewJob.JobName; }
            set { NewJob.JobName = value; OnPropertyChanged("NewJobName"); }
        }

        public string NewJobTermDetails
        {
            get { return NewJob.JobTermDetails; }
            set { NewJob.JobTermDetails = value; OnPropertyChanged("NewJobTermDetails"); }
        }
        public string NewJobSalesTax
        {
            get { return NewJob.JobSalesTax; }
            set { NewJob.JobSalesTax = value; OnPropertyChanged("NewJobSalesTax"); }
        }
        public string NewJobDefaultHeightOfCabinets
        {
            get { return NewJob.JobDefaultHeightOfCabinets; }
            set { NewJob.JobDefaultHeightOfCabinets = value; OnPropertyChanged("NewJobDefaultHeightOfCabinets"); }
        }
        public string NewJobMarkup
        {
            get { return NewJob.JobMarkup; }
            set { NewJob.JobMarkup = value; OnPropertyChanged("NewJobMarkup"); }
        }

        public string NewJobRoomsForJob
        {
            get { return NewJob.JobRoomsForJob; }
            set { NewJob.JobRoomsForJob = value; OnPropertyChanged("NewJobRoomsForJob"); }
        }
        #endregion

        RelayCommand _addJobCommand;

        public ICommand AddJobCommand
        {
            get
            {
                if(_addJobCommand == null)
                {
                    _addJobCommand = new RelayCommand(param => this.AddJob(param));
                }
                return _addJobCommand;
            }
        }

        public void AddJob(object param)
        {

            _jobs.Add(NewJob);
            OnPropertyChanged("Jobs");

            NewJob = new Job();
            OnPropertyChanged("NewJobName");
            OnPropertyChanged("NewJobTermDetails");
            OnPropertyChanged("NewJobSalesTax");
            OnPropertyChanged("NewJobDefaultHeightOfCabinets");
            OnPropertyChanged("NewJobMarkup");
            OnPropertyChanged("NewJobRoomsForJob");
        }
    }
}
