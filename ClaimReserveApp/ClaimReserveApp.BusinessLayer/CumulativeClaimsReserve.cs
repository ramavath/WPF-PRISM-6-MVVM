using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.BusinessLayer.Interface;
using ClaimReserveApp.DAL;
using ClaimReserveApp.DAL.Validator;

namespace ClaimReserveApp.BusinessLayer
{
    public class CumulativeClaimsReserve : ICommand
    {
        private ILoggerFacade logger;
        ObservableCollection<Product> product;
        private string productName = string.Empty;
        private string recentOriginYear;
        private string firstDevelopmentYear;
        private string lastDevelopmentYear;
        private string previsousDevelopmentYear;
        private string previousProductName;
        private string previousOriginYear;
        private ProductValidator validator;
        public CumulativeClaimsReserve(ILoggerFacade logger)
        {
            this.logger = logger;
            validator = new ProductValidator();
        }

        public bool Execute(ref ObservableCollection<CumulativeClaimsData> cumulativeClaimsData, ObservableCollection<Product> product)
        {
            this.product = product;
            firstDevelopmentYear = FirstDevelopmentYear();
            recentOriginYear = RecentOriginYear();
            lastDevelopmentYear = LastDevelopmentYear();

            this.product.OrderBy(where => where.DevelopmentYear);
            previsousDevelopmentYear = "0";
            previousOriginYear = "0";
            previousProductName = string.Empty;
            cumulativeClaimsData = new ObservableCollection<CumulativeClaimsData>();
            
            foreach (Product ProductItem in this.product)
            {

                var results = validator.Validate(ProductItem);
                if (results != null
                       && results.Errors.Count() > 0)
                {
                    foreach (var error in results.Errors)
                    {
                        logger.Log("ProductName : " + ProductItem.ProductName +
                            "Origin Year  : " + ProductItem.OriginYear +
                            "Development Year : " + ProductItem.DevelopmentYear +
                            "Increment Value : " + ProductItem.IncreamentValue +
                            "::" + error.ErrorMessage, Category.Warn, Priority.High);
                    }
                }

                else
                {

                    if (cumulativeClaimsData.Where(where => where.ProductName == ProductItem.ProductName).Count() > 0)
                    {
                        var cumulativedata = cumulativeClaimsData.Where(where => where.ProductName == ProductItem.ProductName).Select(s => s.CumulativeData).FirstOrDefault();
                        cumulativeClaimsData.Where(where => where.ProductName == ProductItem.ProductName).FirstOrDefault()
                                                    .CumulativeData = CumulativeValue(ProductItem.ProductName, ProductItem.OriginYear, ProductItem.DevelopmentYear, ProductItem.IncreamentValue, cumulativedata);
                    }
                    else
                    {
                        cumulativeClaimsData.Add(new CumulativeClaimsData
                        {
                            ProductName = ProductItem.ProductName,
                            DevelopmentYear = lastDevelopmentYear,
                            OriginYear = recentOriginYear,
                            CumulativeData = CumulativeValue(ProductItem.ProductName, ProductItem.OriginYear, ProductItem.DevelopmentYear, ProductItem.IncreamentValue, "")
                        });

                    }

                    previsousDevelopmentYear = ProductItem.DevelopmentYear;
                    previousProductName = ProductItem.ProductName;
                    previousOriginYear = ProductItem.OriginYear;
                }

            }


            return true;
        }



        private string CumulativeValue(string ProductName, string originYear, string developmentYear, double IncrementValue, string cumulativevalue)
        {


            if (Convert.ToInt32(developmentYear) > Convert.ToInt32(firstDevelopmentYear) && string.IsNullOrEmpty(cumulativevalue))
            {
                int counter = CalculateLoopValue(originYear, developmentYear);
                for (int i = 0; i < counter; i++)
                {

                    if (i + 1 == counter)
                        cumulativevalue = cumulativevalue + "0";
                    else
                        cumulativevalue = cumulativevalue + "0" + ",";
                }
                if (cumulativevalue.Contains(","))
                {
                    cumulativevalue = cumulativevalue + "," + (Convert.ToDouble(cumulativevalue.Substring(cumulativevalue.LastIndexOf(',') + 1)) + IncrementValue).ToString();
                }

            }
            else if (developmentYear == firstDevelopmentYear)
            {
                cumulativevalue = IncrementValue.ToString();
            }
            else if (Convert.ToInt32(previsousDevelopmentYear) + 1 < Convert.ToInt32(developmentYear))
            {

                for (int i = 0; i < Convert.ToInt32(developmentYear) - Convert.ToInt32(previsousDevelopmentYear) - 1; i++)
                {
                    cumulativevalue = cumulativevalue + "," + Convert.ToDouble(cumulativevalue.Substring(cumulativevalue.LastIndexOf(',') + 1)).ToString();
                }
                cumulativevalue = cumulativevalue + "," + (Convert.ToDouble(cumulativevalue.Substring(cumulativevalue.LastIndexOf(',') + 1)) + IncrementValue).ToString();
            }
            else if (previousOriginYear != originYear && previousProductName == ProductName)
            {
                cumulativevalue = cumulativevalue + "," + IncrementValue.ToString();
            }

            else
            {

                cumulativevalue = cumulativevalue + "," + (Convert.ToDouble(cumulativevalue.Substring(cumulativevalue.LastIndexOf(',') + 1)) + IncrementValue).ToString();
            }

            return cumulativevalue;

        }

        private int CalculateLoopValue(string originYear, string developmentYear)
        {
            return (((Convert.ToInt32(lastDevelopmentYear) - Convert.ToInt32(firstDevelopmentYear) + 1) * (Convert.ToInt32(originYear) - Convert.ToInt32(firstDevelopmentYear) + 1)) - (Convert.ToInt32(lastDevelopmentYear) - Convert.ToInt32(developmentYear) + 1) - ((((Convert.ToInt32(originYear) - Convert.ToInt32(firstDevelopmentYear) + 1) * (Convert.ToInt32(originYear) - Convert.ToInt32(firstDevelopmentYear) + 1)) - (Convert.ToInt32(originYear) - Convert.ToInt32(firstDevelopmentYear) + 1)) / 2));
        }

        private string RecentOriginYear()
        {

            return product.OrderBy(p => p.OriginYear).Select(s => s.OriginYear).FirstOrDefault();

        }

        private string FirstDevelopmentYear()
        {
            return product.OrderBy(p => p.DevelopmentYear).Select(s => s.DevelopmentYear).FirstOrDefault();
        }

        private string LastDevelopmentYear()
        {
            return product.OrderByDescending(p => p.DevelopmentYear).Select(s => s.DevelopmentYear).FirstOrDefault();
        }

    }
}
