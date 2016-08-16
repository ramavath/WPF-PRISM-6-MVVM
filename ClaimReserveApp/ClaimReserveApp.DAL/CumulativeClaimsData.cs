using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimReserveApp.DAL
{
    public class CumulativeClaimsData : BindableBase
    {
        private string productName;
        private string originYear;
        private string cumulativeData;
        private string developmentYear;
        public string ProductName
        {
            get { return this.productName; }
            set
            {
                SetProperty(ref this.productName, value);
                OnPropertyChanged("ProductName");
            }

        }
        public string OriginYear
        {
            get { return this.originYear; }
            set
            {
                SetProperty(ref this.originYear, value);
                OnPropertyChanged("OriginYear");
            }
        }


        public string DevelopmentYear
        {

            get { return this.developmentYear; }
            set
            {
                SetProperty(ref this.developmentYear, value);
                OnPropertyChanged("FirstDevelopmentYear");
            }
        }

        public string CumulativeData
        {
            get { return this.cumulativeData; }
            set
            {
                SetProperty(ref this.cumulativeData, value);
                OnPropertyChanged("CumulativeData");
            }
        }
    }
}
