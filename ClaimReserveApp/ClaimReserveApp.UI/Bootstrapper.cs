using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using ClaimReserveApp.Data.Adaptor;
using ClaimReserveApp.Data.Interface;
using ClaimReserveApp.UI.Views;
using Prism.Regions;
using Prism.Modularity;
using ClaimReserveApp.BusinessLayer.Interface;
using ClaimReserveApp.BusinessLayer.Adaptor;
using Prism.Logging;

using ClaimReserveApp.DAL.Logger;

namespace ClaimReserveApp.UI
{
    public class Bootstrapper : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();


        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();

        }

        protected override ILoggerFacade CreateLogger()
        {
            return new Log4NetLogger();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeForNavigation<OptionView>("OptionhView");
            Container.RegisterTypeForNavigation<ReserveSummaryView>("ReserveSummaryView");
            Container.RegisterTypeForNavigation<DevelopmentSummaryView>("DevelopmentSummaryView");
            Container.RegisterTypeForNavigation<ReserveEditView>("ReserveEditView");
            //Container.RegisterTypeForNavigation<ProductCumulativeClaimView>("ProductCumulativeClaimView");

            Container.RegisterType<IClaimReserveAppDataAdaptor, ClaimReserveAppDataAdaptor>();
            Container.RegisterType<IBusinessSessionManager, BusinessSessionManager>();

        }
    }
}
