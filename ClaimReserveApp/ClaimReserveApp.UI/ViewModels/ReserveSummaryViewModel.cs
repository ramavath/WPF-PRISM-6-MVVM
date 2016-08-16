using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ClaimReserveApp.DAL.Validator;
using ClaimReserveApp.BusinessLayer.Interface;
using ClaimReserveApp.DAL;
using ClaimReserveApp.Data.Interface;
using ClaimReserveApp.UI.Events;
using FluentValidation;
using Prism.Logging;
using System.Text.RegularExpressions;

namespace ClaimReserveApp.UI.ViewModels
{
    public class ReserveSummaryViewModel : BindableBase
    {

       
        private IClaimReserveAppDataAdaptor dataAdapter;
        private ObservableCollection<Product> position;
        private IEventAggregator eventAggregator;
        private IRegionManager regionManager;
        private string filePath;
        public DelegateCommand<Product> ClickCommand { get; set; }
        public DelegateCommand LocateCSVFileCommand { get; set; }
        private IBusinessSessionManager businessSessionManager;
        private ILoggerFacade logger;
        public ReserveSummaryViewModel(IEventAggregator eventAggregator, 
                                        IClaimReserveAppDataAdaptor dataAdapter, 
                                        IRegionManager regionManager, 
                                        IBusinessSessionManager businessSessionManager,
                                        ILoggerFacade logger)
        {
            this.dataAdapter = dataAdapter;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.businessSessionManager = businessSessionManager;
            this.logger = logger;
            ClickCommand = new DelegateCommand<Product>(Click);
            LocateCSVFileCommand = new DelegateCommand(OpenFileDialog);
           
        }

        private void OpenFileDialog()
        {
            try
            {
                this.filePath = businessSessionManager.FileDialogService.OpenFileDialog();
                if (!string.IsNullOrEmpty(this.filePath))
                {
                    position = dataAdapter.ClaimsReserving.ReadAllClaimsReservingProduct(this.filePath);
                    this.eventAggregator.GetEvent<ClaimReserveEvent>().Publish(position);
                    Position = position;
                }
            }
            catch (Exception ex)
            {
                //logger
                logger.Log(ex.ToString(), Category.Exception, Priority.High);

            }
            

        }

        private void Click(Product obj)
        {
            if (obj != null)
            {

               
                this.eventAggregator.GetEvent<ProductClaimReserveEvent>().Publish(obj);
              //  this.eventAggregator.GetEvent<ClaimReserveEvent>().Publish(position);
            }
        }

        public ObservableCollection<Product> Position {
            get {


                if (position !=null)
                position.OrderBy(x => x.ProductName);
                return this.position;
            }
            set
            {
                
                SetProperty(ref this.position, value);
                OnPropertyChanged("Position");
            }

        }

   
    }
}
