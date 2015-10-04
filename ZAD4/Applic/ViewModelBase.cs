using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace Applic {
    class ViewModelBase : INotifyPropertyChanged {
        //basic ViewModelBase
        internal void RaisePropertyChanged(string prop) {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
