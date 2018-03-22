using System;
using System.Collections.Generic;
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
        public ClientTabVM(){
            Name = "Clients";
        }

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

        }
    }
}
