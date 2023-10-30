using tyf.data.service.Requests;

namespace tyf.data.service.Managers
{
    public interface ICsvManager
    {
        bool UploadContent(UploadCsvRequest request);
    }
}