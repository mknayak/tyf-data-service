using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using tyf.data.service.Exeptions;
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
        private readonly ErrorMessages errorMessages;

        public UtilitiesController(ICsvManager csvManager,IOptions<ErrorMessages> errorMessageOptions) 
        {
            this.csvManager = csvManager;
            this.errorMessages = errorMessageOptions.Value;
        }
        /// <summary>
        /// Uploads a CSV file to the server for a given schema and target namespace.
        /// </summary>
        /// <param name="targetNamespace">The target namespace for the CSV file.</param>
        /// <param name="schemaId">The ID of the schema to upload the CSV file for.</param>
        /// <param name="file">The CSV file to upload.</param>
        /// <returns>A <see cref="StatusResponseModel"/> indicating whether the upload was successful.</returns>
        [HttpPost("{tagetNamespace}/{schemaId}")]
        public StatusResponseModel UploadCsv(string targetNamespace,Guid schemaId, IFormFile file)
        {
            if(file.Length == 0) throw new TechnicalException(errorMessages.Format("CER-102", "File"));
            if(file.Length > 20000000) throw new TechnicalException(errorMessages.Format("CER-104", "size 20MB","File"));
            UploadCsvRequest request = new UploadCsvRequest { File = file, SchemaId = schemaId,TargetNamespace= targetNamespace };
            csvManager.UploadContent(request);

            return new StatusResponseModel { Success = true };
        }
    }
}

