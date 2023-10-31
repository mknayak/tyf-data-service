using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using tyf.data.service.DbModels;
using tyf.data.service.Exeptions;
using tyf.data.service.Extensions;
using tyf.data.service.Requests;
using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    public class DataRepository:IDataRepository
	{
        private readonly TyfDataContext dbContext;
        private readonly ErrorMessages messageOptions;

        public DataRepository(TyfDataContext dbContext, IOptions<ErrorMessages> messageOptions)
		{
            this.dbContext = dbContext;
            this.messageOptions = messageOptions.Value;
        }

        public FieldTypeModel CreateFieldType(CreateFieldTypeRequest request)
        {
            var fieldType = dbContext.SchemaFieldTypes.FirstOrDefault(c => c.FieldTypeName == request.Name);
            if (null != fieldType)
            {
                throw new TechnicalException(messageOptions.Format( Constants.ErrorCodes.Repository.DuplicateEntity,"Field Type"));
            }
            var dbModel = new SchemaFieldType {
                FieldTypeName = request.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate= DateTime.Now,
                FieldTypeDefaultValue= request.DefaultValue
            };

            dbContext.SchemaFieldTypes.Add(dbModel);
            dbContext.SaveChanges();

            return new FieldTypeModel { Name = dbModel.FieldTypeName, Value = dbModel.FieldTypeId };
        }

        public SchemaInstanceModel CreateNewInstance(CreateSchemaInstanceRequest schemaInstanceRequest)
        {
            throw new NotImplementedException();
        }

        public SchemaModel CreateSchema(CreateSchemaRequest request)
        {
            var fullName = request.Namespace + "." + request.Name;
            var model = dbContext.DataSchemas.FirstOrDefault(c => c.SchemaNameSpace == request.Namespace &&
                        c.SchemaName == request.Name);
            if (model != null)
            {
                throw new TechnicalException(messageOptions.Format(Constants.ErrorCodes.Repository.DuplicateEntity, "Schema"));
            }
            var schema = new DataSchema
            {
                SchemaId = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsPublic = true,
                SchemaName = request.Name,
                SchemaNameSpace = request.Namespace
            };
            foreach (var field in request.Fields)
            {
                if (field.Value == null) continue;
                schema.SchemaFields.Add(new SchemaField
                {
                    SchemaFieldId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    SchemaFieldName = field.Value.FieldName,
                    SchemaFieldTypeKey = field.Value.FieldType,
                    SchemaFieldIsRequired=field.Value.IsRequired,
                    SchemaFieldRegex=field.Value.RegexCheck
                });
            }
            dbContext.DataSchemas.Add(schema);
            dbContext.SaveChanges();
            SchemaModel schemaModel = ToSchemaModel(schema);

            return schemaModel;
        }

        private static SchemaModel ToSchemaModel(DataSchema schema)
        {
            var schemaModel = new SchemaModel
            {
                Id = schema.SchemaId,
                FullName = $"{schema.SchemaNameSpace}.{schema.SchemaName}",
                Name = schema.SchemaName

            };
            foreach (var field in schema.SchemaFields)
            {
                schemaModel.Fields.Add(field.SchemaFieldName, new SchemaFieldModel
                {
                    FieldName = field.SchemaFieldName,
                    FieldType = field.SchemaFieldTypeKey,
                    IsRequired = field.SchemaFieldIsRequired,
                    RegexCheck = field.SchemaFieldRegex
                });

            }

            return schemaModel;
        }

        public FieldTypeListModel GetFieldTypes()
        {
            var fieldTypes = dbContext.SchemaFieldTypes.ToList().Select(dbModel => new FieldTypeModel { Name = dbModel.FieldTypeName, Value = dbModel.FieldTypeId });
            return new FieldTypeListModel { Page = 1, Results = fieldTypes, TotalResults = fieldTypes.Count() };
        }

        public SchemaModel GetSchemaById(Guid schemId)
        {
            var dbModel = dbContext.DataSchemas.Where(c => c.SchemaId == schemId).Include(c => c.SchemaFields).FirstOrDefault();
            if(null!= dbModel)
            {
                return ToSchemaModel(dbModel);
            }

            throw new TechnicalException(messageOptions.Format(Constants.ErrorCodes.Repository.EntityNotFound, "Schema"));
            
        }

        public SchemaModel GetSchemaByName(string schemaFullName)
        {
            var nspace = schemaFullName.Substring(0, schemaFullName.LastIndexOf("."));
            var name= schemaFullName.Substring(schemaFullName.LastIndexOf(".")+1);
            var dbModel = dbContext.DataSchemas.Where(c => c.SchemaNameSpace == nspace && c.SchemaName==name).Include(c => c.SchemaFields).FirstOrDefault();
            if (null != dbModel)
            {
                return ToSchemaModel(dbModel);
            }
            throw new TechnicalException(messageOptions.Format(Constants.ErrorCodes.Repository.EntityNotFound, "Schema"));
        }

        public SchemaListModel SearchSchema(string name)
        {
            var dbModels = dbContext.DataSchemas.Where(c => c.SchemaName.Contains(name)).Include(c=>c.SchemaFields).ToList();
            var list = dbModels.Select(c => ToSchemaModel(c));
            return new SchemaListModel { Page = 1, Results = list };
        }

        public SchemaListModel SearchSchemaByField(string fieldName, string fieldValue)
        {
            var dbModels = dbContext.SchemaFields.Where(c => c.SchemaFieldName== fieldName).Select(c=>c.Schema).ToList();
            var list = dbModels.Select(c => ToSchemaModel(c));
            return new SchemaListModel { Page = 1, Results = list };
        }

        public SchemaListModel FilterSchema(string nameSpace)
        {
            var dbModels = dbContext.DataSchemas.Where(c => c.SchemaNameSpace==nameSpace).Include(c => c.SchemaFields).ToList();
            var list = dbModels.Select(c => ToSchemaModel(c));
            return new SchemaListModel { Page = 1, Results = list };
        }
    }
}

