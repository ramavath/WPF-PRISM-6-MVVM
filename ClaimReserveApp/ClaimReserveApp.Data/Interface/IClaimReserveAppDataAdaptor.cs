using ClaimReserveApp.Data.Interface;

namespace ClaimReserveApp.Data.Interface
{
    public interface IClaimReserveAppDataAdaptor
    {
        IClaimsReservingRepo ClaimsReserving { get; set; }
    }
}