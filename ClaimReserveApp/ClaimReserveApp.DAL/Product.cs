using FluentValidation;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ClaimReserveApp.DAL.Validator;

namespace ClaimReserveApp.DAL
{
    public class Product : BindableBase
    {
       // private AbstractValidator<Product> validator;
        public Product()
        {
         //   validator = new ProductValidator();

        }

        private string product = string.Empty;
        public string ProductName
        {
            get { return this.product; }
            set
            {
                SetProperty(ref this.product, value);
                OnPropertyChanged("ProductName");

            }
        }

        private double increamentValue;
        private string developmentYear;
        private string originYear;

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
                OnPropertyChanged("DevelopmentYear");

            }
        }
        public double IncreamentValue
        {
            get { return this.increamentValue; }
            set
            {
                SetProperty(ref this.increamentValue, value);
                OnPropertyChanged("IncreamentValue");

            }
        }

       
    }
}
