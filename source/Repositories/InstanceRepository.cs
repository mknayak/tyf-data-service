using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using tyf.data.service.DbModels;
using tyf.data.service.Exeptions;
using tyf.data.service.Extensions;
using tyf.data.service.Requests;
using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    public class InstanceRepository: IInstanceRepository
    {
        private readonly TyfDataContext dbContext;
        private readonly ErrorMessages messages;

        public InstanceRepository(TyfDataContext dbContext, IOptions<ErrorMessages> messageOptions)
		{
            this.dbContext = dbContext;
            this.messages = messageOptions.Value;
        }

        public bool BulkCreateInstances(BulkCreateInstanceRequest bulkCreateInstanceRequest)
        {
            var schemaId= bulkCreateInstanceRequest.SchemaId;
            var requests = bulkCreateInstanceRequest.Instances;
            var schema = dbContext.DataSchemas.Include(c=>c.SchemaFields).FirstOrDefault(x=>x.SchemaId==schemaId);
            if (null == schema)
            {
                throw new TechnicalException(messages.Format(Constants.ErrorCodes.Repository.EntityNotFound,"Schema"));
            }
            foreach (var request in requests)
            {
                SchemaInstance entity = new SchemaInstance
                {
                    SchemaInstanceId = Guid.NewGuid(),
                    SchemaId = schema.SchemaId,
                    SchemaInstanceNamespace= bulkCreateInstanceRequest.Namespace,
                    SchemaInstanceName = request.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                dbContext.SchemaInstances.Add(entity);
                foreach (var item in request.Fields)
                {
                    var fschema = schema.SchemaFields.FirstOrDefault(c => c.SchemaFieldName == item.Key);
                    if (null != fschema)
                    {
                        entity.SchemaData.Add(new SchemaDatum
                        {
                            SchemaDataId = Guid.NewGuid(),
                            SchemaFieldId = fschema.SchemaFieldId,
                            SchemaInstanceId = entity.SchemaInstanceId,
                            SchemeDataValue = item.Value,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        });
                    }
                }
            }
            dbContext.SaveChanges();
            return true;
        }

        public SchemaInstanceModel CreateInstance(CreateInstanceRequest request)
        {
            var schema = dbContext.DataSchemas.Include(c=>c.SchemaFields).FirstOrDefault(x=>x.SchemaId==request.SchemaId);
            if (null == schema)
            {
                throw new TechnicalException(messages.Format(Constants.ErrorCodes.Repository.EntityNotFound,"Schema"));
            }

            SchemaInstance entity = new SchemaInstance
            {
                SchemaInstanceId = Guid.NewGuid(),
                SchemaId = schema.SchemaId,
                SchemaInstanceNamespace= request.Namespace,
                SchemaInstanceName = request.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            dbContext.SchemaInstances.Add(entity);

            dbContext.SaveChanges();
            return new SchemaInstanceModel
            {
                Name = request.Name,
                Id = entity.SchemaInstanceId,
                Fields = schema.SchemaFields.
                ToDictionary(x => x.SchemaFieldName, x => string.Empty)
            };
        }

        public InstanceListModel FilterInstances(FilterInstanceRequest request)
        {
            var instances = dbContext.SchemaInstances.Where(c => c.SchemaInstanceNamespace == request.Namespace)
                .Include(x => x.SchemaData).ThenInclude(x=>x.SchemaField).ToList();
            var instanceResponses = instances.Select(c => new SchemaInstanceModel
            {
                Name = c.SchemaInstanceName??string.Empty,
                Id = c.SchemaInstanceId,
                Fields = c.SchemaData.
                ToDictionary(x => x.SchemaField.SchemaFieldName, x => x.SchemeDataValue??string.Empty)
            });

            return new InstanceListModel { Results = instanceResponses };
        }

        public SchemaInstanceModel GetInstance(Guid instanceId)
        {
            var instance = dbContext.SchemaInstances.Find(instanceId);
            if (null == instance)
            {
                throw new TechnicalException(messages.Format(Constants.ErrorCodes.Repository.EntityNotFound, "Instance"));
            }
            var fields = dbContext.SchemaData.Where(c => c.SchemaInstanceId == instanceId)
                        .Join(dbContext.SchemaFields, x => x.SchemaFieldId, y => y.SchemaFieldId, (x, y) => new {x.SchemeDataValue,y.SchemaFieldName })
                        .ToList();
            return new SchemaInstanceModel
            {
                Name = instance.SchemaInstanceName,
                Id = instance.SchemaInstanceId,
                Fields = fields.ToDictionary(c=>c.SchemaFieldName,c=>c.SchemeDataValue??string.Empty)
            };
        }

        public SchemaInstanceModel UpdateInstance(UpdateInstanceRequest request)
        {
            var instance = dbContext.SchemaInstances.Find(request.InstanceId);
            if (null == instance)
            {
                throw new TechnicalException(messages.Format(Constants.ErrorCodes.Repository.EntityNotFound, "Instance"));
            }
            var schema = dbContext.SchemaFields.Where(x => x.SchemaId == instance.SchemaId).ToList();
            Dictionary<string, string> fields = new Dictionary<string, string>();
            foreach (var item in request.Fields)
            {
                var fschema = schema.FirstOrDefault(c => c.SchemaFieldName == item.Key);
                if (null != fschema)
                {
                    instance.SchemaData.Add(new SchemaDatum
                    {
                        SchemaDataId = Guid.NewGuid(),
                        SchemaFieldId = fschema.SchemaFieldId,
                        SchemaInstanceId = instance.SchemaInstanceId,
                        SchemeDataValue = item.Value,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });
                    fields.Add(fschema.SchemaFieldName, item.Value);
                }
            }
            dbContext.SaveChanges();
            return new SchemaInstanceModel
            {
                Name = instance.SchemaInstanceName,
                Id = instance.SchemaInstanceId,
                Fields = fields
            };
        }
    }
}

