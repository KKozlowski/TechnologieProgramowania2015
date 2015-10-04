using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public interface IFiller
    {
        void Fill(List<Reader> lst, Dictionary<int, Book> dic, ObservableCollection<Borrow> oc);
        
    }
}
