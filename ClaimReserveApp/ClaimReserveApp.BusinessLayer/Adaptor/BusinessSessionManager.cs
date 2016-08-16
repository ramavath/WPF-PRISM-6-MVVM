using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.BusinessLayer.Interface;

namespace ClaimReserveApp.BusinessLayer.Adaptor
{
    public class BusinessSessionManager : IBusinessSessionManager
    {
        private ICommand cumulativeClaimsReserve;
        private IOService fileDialogService;
        ILoggerFacade logger;
        public BusinessSessionManager(ILoggerFacade logger)
        {
            this.logger = logger;
        }
        public ICommand CumulativeClaimsReserve
        {
            get
            {

                if (this.cumulativeClaimsReserve == null)
                {
                    return this.cumulativeClaimsReserve = new CumulativeClaimsReserve(logger);
                }
                else return this.cumulativeClaimsReserve;

            }

            set
            {
                ;
            }

        }

        public IOService FileDialogService
        {
            get
            {

                if (this.fileDialogService == null)
                {
                    return this.fileDialogService = new FileDialogService(logger);
                }
                else return this.fileDialogService;

            }

            set
            {
                ;
            }

        }
    }
}
