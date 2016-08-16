using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.Data.Interface;
using ClaimReserveApp.Data.Repository;

namespace ClaimReserveApp.Data.Adaptor
{
    public class ClaimReserveAppDataAdaptor : IClaimReserveAppDataAdaptor
    {
        IClaimsReservingRepo claimsReserving;
        public ClaimReserveAppDataAdaptor()
        {

        }

        public IClaimsReservingRepo ClaimsReserving
        {
            get
            {

                if (this.claimsReserving == null)
                {
                    return this.claimsReserving = new ClaimsReservingRepo();
                }
                else return this.claimsReserving;

            }

            set
            {
                this.claimsReserving = value;
            }
        }
    }
}
