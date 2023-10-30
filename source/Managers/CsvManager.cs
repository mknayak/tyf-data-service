using System;
using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using tyf.data.service.DbModels;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

namespace tyf.data.service.Managers
{
    public class CsvManager : ICsvManager
    {
        private readonly TyfDataContext context;
        private readonly IInstanceRepository instanceRepository;
        private readonly IDataRepository dataRepository;

        public CsvManager(TyfDataContext context, IInstanceRepository repository, IDataRepository dataRepository)
        {
            this.context = context;
            this.instanceRepository = repository;
            this.dataRepository = dataRepository;
        }

        public bool UploadContent(UploadCsvRequest request)
        {
            var schema = dataRepository.GetSchemaById(request.SchemaId);
            if (null == schema) return false;

            using (var reader = new StreamReader(request.File.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                
                while (csv.Read())
                {
                    var record = csv.GetRecord<dynamic>();
                    // Do something with the record.
                    var instance=instanceRepository.CreateInstance(new CreateInstanceRequest
                    {
                        Name = csv.GetField("Name"),
                        Namespace = request.TargetNamespace,
                        SchemaId = schema.Id
                    });
                    var fields = new Dictionary<string, string>();
                    foreach (var header in csv.HeaderRecord)
                    {
                        fields.TryAdd(header, csv.GetField(header));
                    }
                    instanceRepository.UpdateInstance(new UpdateInstanceRequest
                    {
                        InstanceId = instance.Id,
                        Fields= fields
                    });
                }
            }
            return true;
        }
    }
}

