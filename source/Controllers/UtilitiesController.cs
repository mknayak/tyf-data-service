using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Managers;
using tyf.data.service.Models;
using tyf.data.service.Requests;


namespace tyf.data.service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ValidateAPIAccess(Constants.Roles.Administrator)]
    public class UtilitiesController : ControllerBase
    {
        private readonly ICsvManager csvManager;

        public UtilitiesController(ICsvManager csvManager)
        {
            this.csvManager = csvManager;
        }
        [HttpPost("{targetns}/{schemaId}")]
        public StatusResponseModel UploadCsv(string targetns,Guid schemaId, IFormFile file)
        {

            UploadCsvRequest request = new UploadCsvRequest { File = file, SchemaId = schemaId,TargetNamespace= targetns };
            csvManager.UploadContent(request);

            return new StatusResponseModel { Success = true };
        }
    }
}

