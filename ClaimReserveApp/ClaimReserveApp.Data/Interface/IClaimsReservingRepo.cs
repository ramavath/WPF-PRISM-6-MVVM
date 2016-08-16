using System.Collections.ObjectModel;
using System.Linq;
using ClaimReserveApp.DAL;

namespace ClaimReserveApp.Data.Interface
{
   public interface IClaimsReservingRepo
    {
        bool DeleteClaimsReservingProduct(string productName);
        bool InsertClaimsReservingProduct(Product product);
        ObservableCollection<Product> ReadAllClaimsReservingProduct(string filePath);
        Product ReadClaimsReservingProductId(int id);
        bool UpdateClaimsReservingProduct(int id);
    }
}