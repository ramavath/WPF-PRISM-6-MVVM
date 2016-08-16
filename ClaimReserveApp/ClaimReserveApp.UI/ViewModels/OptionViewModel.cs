using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserveApp.UI.ViewModels
{
    public class OptionViewModel: BindableBase
    {


        public DelegateCommand<string> NavigateCommand { get; set; }
        private readonly IRegionManager regionManager;
        public OptionViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string Url)
        {
            if (Url == "ReserveSummaryView")
            {
               regionManager.RequestNavigate(RegionNames.MainRegion, Url);
               
            }
            else if(Url == "DevelopmentSummaryView")
            {
                
                regionManager.RequestNavigate(RegionNames.MainRegion, Url);

            }
        }

     }
}
