using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClaimReserveApp.BusinessLayer;
using Prism.Logging;
using ClaimReserveApp.DAL.Logger;
using ClaimReserveApp.Data.Interface;
using ClaimReserveApp.Data.Adaptor;
using System.Collections.ObjectModel;
using ClaimReserveApp.DAL;

namespace ClaimReserveApp.Test
{
    [TestClass]
    public class ClaimReserveAppTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            IClaimReserveAppDataAdaptor dataAdapter = new ClaimReserveAppDataAdaptor();
            ObservableCollection<Product> position = dataAdapter.ClaimsReserving.ReadAllClaimsReservingProduct(@"C:\Govind\ClaimReserveApp\Worksheet in Developer Problem WPF.csv");
            ObservableCollection<CumulativeClaimsData> cumulativeData = new ObservableCollection<CumulativeClaimsData>();
            CumulativeClaimsReserve claimR = new CumulativeClaimsReserve(new Log4NetLogger());
            bool Valid = claimR.Execute(ref cumulativeData, position);
            Assert.AreEqual<bool>(true, Valid, "Invalid Value");

        }
    }
}
