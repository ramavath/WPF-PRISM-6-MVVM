using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.DAL;

namespace ClaimReserveApp.BusinessLayer.Interface
{
    public interface ICommand
    {
        bool Execute(ref ObservableCollection<CumulativeClaimsData> cumulativeData, ObservableCollection<Product> product);
    }
}
