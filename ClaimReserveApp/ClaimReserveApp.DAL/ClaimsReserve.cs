using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserveApp.DAL
{
    public class ClaimsReserve : BindableBase
    {
        private string increamentValue = string.Empty;
        private string developmentYear = string.Empty;
        private string originYear = string.Empty;

        public string OriginYear
        {
            get { return this.originYear; }
            set { SetProperty(ref this.originYear, value); }
        }


        public string DevelopmentYear
        {
            get { return this.developmentYear; }
            set { SetProperty(ref this.developmentYear, value); }
        }
        public string IncreamentValue
        {
            get { return this.increamentValue; }
            set { SetProperty(ref this.increamentValue, value); }
        }
    }
}
