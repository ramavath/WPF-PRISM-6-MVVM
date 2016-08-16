using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.UI.Views;
namespace ClaimReserveApp.UI
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private ILoggerFacade logger;
        public ShellViewModel(IUnityContainer container, IRegionManager regionManager, ILoggerFacade logger)
        {
            logger.Log("ShellViewModel Configured started", Category.Info, Priority.None);
            this.regionManager = regionManager;
            this.container = container;
            this.regionManager.RegisterViewWithRegion(RegionNames.MainToolBarRegion,
                                                      () => this.container.Resolve<OptionView>());
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion,
                                                     () => this.container.Resolve<ReserveSummaryView>());
            this.regionManager.RegisterViewWithRegion(RegionNames.MainRegion,
                                                     () => this.container.Resolve<DevelopmentSummaryView>());
            this.regionManager.RegisterViewWithRegion(RegionNames.ActionRegion,
                                                     () => this.container.Resolve<ReserveEditView>());
            logger.Log("ShellViewModel Configured ended", Category.Info, Priority.None);
            // this.regionManager.RegisterViewWithRegion(RegionNames.ProductRearchRegion,
            //                                          () => this.container.Resolve<ProductCumulativeClaimView>());

        }
    }
}
