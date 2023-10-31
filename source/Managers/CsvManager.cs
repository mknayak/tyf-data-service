using System.Globalization;
using CsvHelper;
using tyf.data.service.Exeptions;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

namespace tyf.data.service.Managers
{
    public class CsvManager : ICsvManager
    {
        private readonly IInstanceRepository instanceRepository;
        private readonly IDataRepository dataRepository;

        public CsvManager(IInstanceRepository repository, IDataRepository dataRepository)
        {
            this.instanceRepository = repository;
            this.dataRepository = dataRepository;
        }

        public bool UploadContent(UploadCsvRequest request)
        {
            var schema = dataRepository.GetSchemaById(request.SchemaId);
            if (null == schema) throw new TechnicalException("CER-102", "Schema not found");
            int maxBulkInsert=200;
            using (var reader = new StreamReader(request.File.OpenReadStream()))
            
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                List<BulkCreateInstanceRequestItem> requests = new List<BulkCreateInstanceRequestItem>();
                int count = 0;
                while (csv.Read())
                {
                    if(null == csv.HeaderRecord) throw new Exception("Header record is null");
                    var nameField=csv.GetField("Name");
                    if(null == nameField) throw new Exception("Name field is null");
                    var fields = new Dictionary<string, string>();
                    foreach (var header in csv.HeaderRecord)
                    {
                        string? value = csv.GetField(header);
                        if(null != value)
                            fields.TryAdd(header, value);
                    }
                    requests.Add(new BulkCreateInstanceRequestItem
                    {
                        Name = nameField,
                        Fields = fields
                    });
                    if (count == maxBulkInsert)
                    {
                        instanceRepository.BulkCreateInstances(new BulkCreateInstanceRequest
                        {
                            SchemaId = schema.Id,
                            Namespace = request.TargetNamespace,
                            Instances = requests
                        });
                        count = 0;
                        requests = new List<BulkCreateInstanceRequestItem>();
                    }
                    count++;
                }
            
                if(requests.Count>0)
                {
                    instanceRepository.BulkCreateInstances(new BulkCreateInstanceRequest
                    {
                        SchemaId = schema.Id,
                        Namespace = request.TargetNamespace,
                        Instances = requests
                    });
                }
            }
            return true;
        }
    }
}

