using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using tyf.data.service.DbModels;
using tyf.data.service.Exeptions;
using tyf.data.service.Extensions;
using tyf.data.service.Requests;
using tyf.data.service.Models;
using Models.Stats;

namespace tyf.data.service.Repositories
{
    /// <summary>
    /// Repository class for managing data schemas and field types.
    /// </summary>
    public class SchemaRepository:ISchemaRepository
	{
        private readonly TyfDataContext dbContext;
        private readonly ErrorMessages messageOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="messageOptions"></param>
        public SchemaRepository(TyfDataContext dbContext, IOptions<ErrorMessages> messageOptions)
		{
            this.dbContext = dbContext;
            this.messageOptions = messageOptions.Value;
        }
        /// <summary>
        /// Creates a new field type.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
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
        /// <summary>
        /// Creates a new schema.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
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
        /// <summary>
        /// Converts a <see cref="DataSchema"/> to a <see cref="SchemaModel"/>.
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the list of available field types.
        /// </summary>
        /// <returns></returns>
        public FieldTypeListModel GetFieldTypes()
        {
            var fieldTypes = dbContext.SchemaFieldTypes.ToList().Select(dbModel => new FieldTypeModel { Name = dbModel.FieldTypeName, Value = dbModel.FieldTypeId });
            return new FieldTypeListModel { Page = 1, Results = fieldTypes, TotalResults = fieldTypes.Count() };
        }
        /// <summary>
        /// Gets a schema by its ID.
        /// </summary>
        /// <param name="schemId"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public SchemaModel GetSchemaById(Guid schemId)
        {
            var dbModel = dbContext.DataSchemas.Where(c => c.SchemaId == schemId).Include(c => c.SchemaFields).FirstOrDefault();
            if(null!= dbModel)
            {
                return ToSchemaModel(dbModel);
            }

            throw new TechnicalException(messageOptions.Format(Constants.ErrorCodes.Repository.EntityNotFound, "Schema"));
            
        }
        /// <summary>
        /// Gets a schema by its full name.
        /// </summary>
        /// <param name="schemaFullName"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
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
        /// <summary>
        /// Searches for schemas.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SchemaListModel SearchSchema(SearchSchemaRequest request)
        {
            IQueryable<DataSchema> dbModels = dbContext.DataSchemas.AsQueryable();
            IQueryable<DataSchema> dbModelsCount = dbContext.DataSchemas.AsQueryable();
            if(!string.IsNullOrEmpty(request.Name))
            {
                dbModels = dbModels.Where(c => c.SchemaName.Contains(request.Name));
                dbModelsCount = dbModelsCount.Where(c => c.SchemaName.Contains(request.Name));
            }
            if(!string.IsNullOrEmpty(request.Namespace))
            {
                dbModels = dbModels.Where(c => c.SchemaNameSpace.Contains(request.Namespace));
                dbModelsCount = dbModelsCount.Where(c => c.SchemaNameSpace.Contains(request.Namespace));
            }            
            var matchingResult= dbModelsCount.Count();
            var list = dbModels.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList().Select(c => ToSchemaModel(c));
            
            return new SchemaListModel { 
                Page = request.Page, 
                Results = list , 
                TotalResults= matchingResult};
        }
        /// <summary>
        /// Retrieves statistics for the schema.
        /// </summary>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public SchemaStatisticsModel GetSchemaStatistics(Guid schemaId)
        {
            var schema = dbContext.DataSchemas.Where(c => c.SchemaId == schemaId).Include(c => c.SchemaFields).FirstOrDefault();
            if (null == schema)
            {
                throw new TechnicalException(messageOptions.Format(Constants.ErrorCodes.Repository.EntityNotFound, "Schema"));
            }
            var model = new SchemaStatisticsModel
            {
                Id = schema.SchemaId,
                SchemaFullName = schema.SchemaNameSpace+"."+ schema.SchemaName,
                LastUpdated = schema.UpdatedDate,
                LastInstanceCreated = dbContext.SchemaInstances.Where(c => c.SchemaId == schema.SchemaId).OrderByDescending(c => c.CreatedDate).Select(c => c.CreatedDate).FirstOrDefault(),
                InstanceCount = dbContext.SchemaInstances.Where(c => c.SchemaId == schema.SchemaId).Count()              
            };
            return model;
        }
    }
}

