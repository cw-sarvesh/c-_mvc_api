using UsedCar.Models.CertificationModel;

namespace UsedCar.Interface.CertificationReport
{
    public interface ICertificationReport
    {
        Task<List<CertificationModel>> GetAllInfo();

    }
}