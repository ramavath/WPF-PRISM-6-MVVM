using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.DAL;

namespace ClaimReserveApp.UI.Events
{
    public class ClaimReserveEvent : PubSubEvent<ObservableCollection<Product>>
    {
    }
}
