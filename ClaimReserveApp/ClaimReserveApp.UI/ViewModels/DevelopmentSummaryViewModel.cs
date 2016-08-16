using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.DAL;
using ClaimReserveApp.Data.Interface;
using ClaimReserveApp.UI.Events;
using ClaimReserveApp.BusinessLayer.Interface;

namespace ClaimReserveApp.UI.ViewModels
{
    public class DevelopmentSummaryViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;
        private IClaimReserveAppDataAdaptor dataAdapter;
        private IRegionManager regionManager;
        private ObservableCollection<CumulativeClaimsData> cumulativeClaimsData;
        private IBusinessSessionManager businessSessionManager;
        public DevelopmentSummaryViewModel(IEventAggregator eventAggregator, IClaimReserveAppDataAdaptor dataAdapter, IRegionManager regionManager, IBusinessSessionManager businessSessionManager)
        {
            this.eventAggregator = eventAggregator;
            this.dataAdapter = dataAdapter;
            this.regionManager = regionManager;
            this.businessSessionManager = businessSessionManager;
            this.eventAggregator.GetEvent<ClaimReserveEvent>().Subscribe(SaveClaimReserve, ThreadOption.UIThread, true);
        }

        private void SaveClaimReserve(ObservableCollection<Product> position)
        {
            if (position != null)
            {

                if (businessSessionManager.CumulativeClaimsReserve.Execute(ref cumulativeClaimsData, position))
                {
                    CumulativeClaimsData = cumulativeClaimsData;
                }


            }

        }

        public ObservableCollection<CumulativeClaimsData> CumulativeClaimsData
        {
            get
            {

                return cumulativeClaimsData;
            }
            set
            {
                SetProperty(ref this.cumulativeClaimsData, value);
                OnPropertyChanged("CumulativeClaimsData");
            }

        }
    }
}
