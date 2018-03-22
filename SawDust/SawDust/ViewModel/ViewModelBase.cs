using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI;

namespace SawDust.ViewModel
{
    class ViewModelBase : DependencyObject, INotifyPropertyChanged, IDisposable
    {
        private string _name;
        private Color _backgroundColor = Colors.Transparent;
        
        #region Constructor
        protected ViewModelBase() { }
        #endregion

        #region DisplayName
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        #endregion

        #region  INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (null != handler)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }



        #endregion

        #region IDisposable
        public void Dispose()
        {
            this.OnDispose();
        }


        protected virtual void OnDispose()
        {
        }
        #endregion
    }
}
