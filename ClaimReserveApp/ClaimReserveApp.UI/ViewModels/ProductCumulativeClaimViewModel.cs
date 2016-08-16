using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserveApp.UI.ViewModels
{
    public class ProductCumulativeClaimViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;
        private IRegionManager regionManager;
        public ProductCumulativeClaimViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;

        }
    }
}
