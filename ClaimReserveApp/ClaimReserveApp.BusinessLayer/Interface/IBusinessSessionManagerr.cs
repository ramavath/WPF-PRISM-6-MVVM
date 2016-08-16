using ClaimReserveApp.BusinessLayer.Interface;

namespace ClaimReserveApp.BusinessLayer.Interface
{
    public interface IBusinessSessionManager
    {
        ICommand CumulativeClaimsReserve { get; set; }
        IOService FileDialogService { get; set; }
    }
}