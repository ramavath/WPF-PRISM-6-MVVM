using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using ClaimReserveApp.DAL;
using ClaimReserveApp.UI.Events;
using Prism.Logging;
using Prism.Interactivity.InteractionRequest;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ClaimReserveApp.UI.ViewModels
{
    public class ReserveEditViewModel : BindableBase, IDataErrorInfo
    {
        private IEventAggregator eventAggregator;
        private IRegionManager regionManager;
        private Product product;
        public InteractionRequest<IConfirmation> NotificationRequest { get; private set; }
        public DelegateCommand ProductSaveCommand { get; set; }
        private ObservableCollection<Product> Position;
        private ILoggerFacade logger;
        private string productName;
        private string originYear;
        private string developmentYear;
        private double increament;


        public ReserveEditViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ILoggerFacade logger)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<ProductClaimReserveEvent>().Subscribe(Updated, ThreadOption.UIThread, true);
            this.eventAggregator.GetEvent<ClaimReserveEvent>().Subscribe(ClaimReserve, ThreadOption.UIThread, true);
            ProductSaveCommand = new DelegateCommand(SaveProducd, CanSave);
             this.logger = logger;
            NotificationRequest = new InteractionRequest<IConfirmation>();
        }

        private bool CanSave()
        {

            try
            {
                if (product != null)
                {
                    if (!string.IsNullOrEmpty(developmentYear)
                    && !string.IsNullOrEmpty(productName)
                    && !string.IsNullOrEmpty(originYear)
                    && (Convert.ToInt32(developmentYear) >= Convert.ToInt32(originYear))
                    && increament > 0)
                    {
                        return developmentYear != product.DevelopmentYear
                                || productName  != product.ProductName
                                || originYear != product.OriginYear
                                || increament != product.IncreamentValue;
                    }
                    else return false;
                        
                }
                else return false;
            }
            catch (Exception ex)
            {
                //logger
                logger.Log(ex.ToString(), Category.Exception, Priority.High);
                return false;
            }



        }

        private void ClaimReserve(ObservableCollection<Product> obj)
        {
            try
            {
                Position = obj;
            }
            catch (Exception ex)
            {

                //logger
                logger.Log(ex.ToString(), Category.Exception, Priority.High);
            }

        }

        private void Updated(Product obj)
        {
            try
            {
               
                ProductName = obj.ProductName;
                OriginYear = obj.OriginYear;
                DevelopmentYear = obj.DevelopmentYear;
                Increament = obj.IncreamentValue;
                product = obj;
            }
            catch (Exception ex)
            {

                //logger
                logger.Log(ex.ToString(), Category.Exception, Priority.High);
            }


        }

        private void SaveProducd()
        {
            try
            {
                product.ProductName = productName;
                product.DevelopmentYear = developmentYear;
                product.OriginYear = originYear;
                product.IncreamentValue = increament;
                // Position.RemoveAt(product.ProductKey);
                
                Position.Add(product);
                this.eventAggregator.GetEvent<ClaimReserveEvent>().Publish(Position);
                RaiseNotification();
                ProductSaveCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                //logger
                logger.Log(ex.ToString(), Category.Exception, Priority.High);
            }
        }
        public string ProductName {
            get
            {
                return this.productName;
            }
            set
            {
                productName = value;
                SetProperty(ref this.productName, value);
                ProductSaveCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("ProductName");
            }
        }
        public string OriginYear {
            get
            {
                return this.originYear;
            }
            set
            {
                originYear = value;
                SetProperty(ref this.originYear, value);
                ProductSaveCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("OriginYear");
            }
        }
        public string DevelopmentYear
        {
            get
            {
                return this.developmentYear;
            }
            set
            {
                developmentYear = value;
                SetProperty(ref this.developmentYear, value);
                ProductSaveCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("DevelopmentYear");
            }
        }
        public double Increament
        {
            get
            {
                return this.increament;
            }

            set
            {
                increament = value;
                SetProperty(ref this.increament, value);
                ProductSaveCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Increament");

            }
        }

        private void RaiseNotification()
        {
            this.NotificationRequest.Raise(
               new Confirmation
               {
                   Content = "Product has been saved succesfully",
                   Title = "Saved"
               }
               );
        }

        #region IDataErrorInfo 
        public string Error
        {
            get
            {
                
                if (Convert.ToInt32(this["OriginYear"]) > Convert.ToInt32(this["DevelopmentYear"]))
                {
                    return "Development year be after start origin year";
                }

                else return this["ProductName"] != null 
                     || this["DevelopmentYear"] != null 
                     || this["OriginYear"] != null 
                     || this["Increament"] != null ? "Correct values" : null;
              }
        }

        public string this[string propertyName]
        {
            get
            {
                
                if (product != null)
                {
                    if (propertyName == "ProductName")
                    {
                        if (string.IsNullOrEmpty(ProductName))
                        {
                            return "Product Name is required.";
                        }
                    }
                    if (propertyName == "DevelopmentYear")
                    {
                        if (string.IsNullOrEmpty(DevelopmentYear) || !ValidateYear(DevelopmentYear))
                        {
                            return "Development Year is required.";
                        }
                        else if (Convert.ToInt32(OriginYear) > Convert.ToInt32(DevelopmentYear))
                        {
                            return "Development year must be after start origin year";
                        }
                    }
                    if (propertyName == "OriginYear")
                    {
                        if (string.IsNullOrEmpty(OriginYear) || !ValidateYear(OriginYear))
                        {
                            return "OriginYear Year is required.";
                        }
                        //else if (Convert.ToInt32(OriginYear) > Convert.ToInt32(DevelopmentYear))
                        //{
                        //    return "Development year be after start origin year";
                        //}
                    }
                    if (propertyName == "Increament")
                    {
                        if (Increament < 0)
                        {
                            return "Increament Value is required.";
                        }
                    }
                }
                return null;
            }
        }

      
        private bool ValidateYear(string arg)
        {
            return (Regex.IsMatch(arg, "^[12][0-9]{3}$"));
        }

        #endregion IDataErrorInfo
    }
}
