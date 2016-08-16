using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimReserveApp.DAL;
using ClaimReserveApp.Data.Interface;

namespace ClaimReserveApp.Data.Repository
{
    internal sealed class ClaimsReservingRepo : IClaimsReservingRepo
    {
        private string CSVFilePath = string.Empty;
        public ClaimsReservingRepo()
        {

        }

        public bool DeleteClaimsReservingProduct(string productName)
        {
            throw new NotImplementedException();
        }

        public bool InsertClaimsReservingProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool InsertClaimsReservingProduct(ClaimsReserve claimReserve)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Product> ReadAllClaimsReservingProduct(string filePath)
        {

            try
            {
                int count = 0;
                var claimserverData = File.ReadAllLines(filePath)
                                  .Skip(1)
                                  .Select(record => record.Split(','))
                                  .OrderBy(column => column[0]);

                return (claimserverData
                                  .Select(records =>
                                   new Product()
                                   {

                                      // ProductKey = count++,
                                       ProductName = records[0],
                                       OriginYear = records[1],
                                       DevelopmentYear = records[2],
                                       IncreamentValue = Convert.ToDouble(records[3])
                                       //ClaimReserve = (from subRecord in claimserverData
                                       //                where (subRecord[0] == records[0])
                                       //                select new ClaimsReserve()
                                       //                {
                                       //                    OriginYear = subRecord[1],
                                       //                    DevelopmentYear = subRecord[2],
                                       //                    IncreamentValue = subRecord[3]
                                       //                }
                                       //).ToList().ToObservableCollection()

                                   }

                                  )).ToObservableCollection();
            }
            finally
            {


            }



        }


        public Product ReadClaimsReservingProductId(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateClaimsReservingProduct(int id)
        {
            throw new NotImplementedException();
        }


    }
}
