
CREATE SCHEMA IF NOT EXISTS data
    AUTHORIZATION postgres;
CREATE SCHEMA IF NOT EXISTS master
    AUTHORIZATION postgres;

-- Table: master.AccessAccounts

-- DROP TABLE IF EXISTS master."AccessAccounts";

CREATE TABLE IF NOT EXISTS master."AccessAccounts"
(
    "AccessAccountId" uuid NOT NULL,
    "AccessAccountName" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "AccessAccountEmail" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "AccessAccountSalt" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "AccessAccountPasswordhash" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "AccessAccountIsLocked" boolean NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "AccessAccounts_pkey" PRIMARY KEY ("AccessAccountId")
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."AccessAccounts"
    OWNER to postgres;

-- Table: master.AccessRole

-- DROP TABLE IF EXISTS master."AccessRole";

CREATE TABLE IF NOT EXISTS master."AccessRole"
(
    "AccessRoleId" uuid NOT NULL,
    "AccessRoleName" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "AccessRole_pkey" PRIMARY KEY ("AccessRoleId")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."AccessRole"
    OWNER to postgres;

-- Table: master.AccessAccountRoles

-- DROP TABLE IF EXISTS master."AccessAccountRoles";

CREATE TABLE IF NOT EXISTS master."AccessAccountRoles"
(
    "AccessAccountRoleId" uuid NOT NULL,
    "AccessAccountId" uuid NOT NULL,
    "AccessRoleId" uuid NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "AccessAccountRoles_pkey" PRIMARY KEY ("AccessAccountRoleId"),
    CONSTRAINT fk_accessaccountrole_accessaccount FOREIGN KEY ("AccessAccountId")
        REFERENCES master."AccessAccounts" ("AccessAccountId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_accessaccountrole_accessrole FOREIGN KEY ("AccessRoleId")
        REFERENCES master."AccessRole" ("AccessRoleId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."AccessAccountRoles"
    OWNER to postgres;





-- Table: master.AccessGroup

-- DROP TABLE IF EXISTS master."AccessGroup";

CREATE TABLE IF NOT EXISTS master."AccessGroup"
(
    "AccessGroupId" uuid NOT NULL,
    "AccessGroupName" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "AccessGroupNamespace" character varying(250) COLLATE pg_catalog."default" NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "AccessGroup_pkey" PRIMARY KEY ("AccessGroupId"),
    CONSTRAINT uk_accessgroup UNIQUE ("AccessGroupName", "AccessGroupNamespace")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."AccessGroup"
    OWNER to postgres;

-- Table: master.AccessGroupAccounts

-- DROP TABLE IF EXISTS master."AccessGroupAccounts";

CREATE TABLE IF NOT EXISTS master."AccessGroupAccounts"
(
    "AccessGroupAccountId" uuid NOT NULL,
    "AccessAccountId" uuid NOT NULL,
    "AccessGroupId" uuid NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "AccessGroupAccount_pkey" PRIMARY KEY ("AccessGroupAccountId"),
    CONSTRAINT fk_accessgroupaccount_accessaccount FOREIGN KEY ("AccessAccountId")
        REFERENCES master."AccessAccounts" ("AccessAccountId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_accessgroupaccount_accessgroup FOREIGN KEY ("AccessGroupId")
        REFERENCES master."AccessGroup" ("AccessGroupId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."AccessGroupAccounts"
    OWNER to postgres;

-- Table: master.AccessGroupRoles

-- DROP TABLE IF EXISTS master."AccessGroupRoles";

CREATE TABLE IF NOT EXISTS master."AccessGroupRoles"
(
    "AccessGroupRoleId" uuid NOT NULL,
    "AccessGroupId" uuid NOT NULL,
    "AccessRoleId" uuid NOT NULL,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "AccessGroupRoles_pkey" PRIMARY KEY ("AccessGroupRoleId"),
    CONSTRAINT fk_accessgrouprole_accessgroup FOREIGN KEY ("AccessGroupId")
        REFERENCES master."AccessGroup" ("AccessGroupId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_accessgrouprole_accessrole FOREIGN KEY ("AccessRoleId")
        REFERENCES master."AccessRole" ("AccessRoleId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."AccessGroupRoles"
    OWNER to postgres;


-- Table: master.DataSchema

-- DROP TABLE IF EXISTS master."DataSchema";

CREATE TABLE IF NOT EXISTS master."DataSchema"
(
    "SchemaId" uuid NOT NULL,
    "SchemaName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SchemaNameSpace" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "IsPublic" boolean NOT NULL DEFAULT true,
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    CONSTRAINT "DataSchema_pkey" PRIMARY KEY ("SchemaId"),
    CONSTRAINT "DataSchema_SchemaNameSpace_SchemaName_key" UNIQUE ("SchemaNameSpace", "SchemaName")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."DataSchema"
    OWNER to postgres;
-- Table: master.Configurations

-- DROP TABLE IF EXISTS master."Configurations";

CREATE TABLE IF NOT EXISTS master."Configurations"
(
    "ConfigurationKey" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ConfigurationType" smallint DEFAULT 0,
    "ConfigurationVersion" numeric NOT NULL,
    "ConfigurationValue" character varying(500) COLLATE pg_catalog."default",
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Configurations_pkey" PRIMARY KEY ("ConfigurationKey")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."Configurations"
    OWNER to postgres;





-- Table: data.SchemaInstance

-- DROP TABLE IF EXISTS data."SchemaInstance";

CREATE TABLE IF NOT EXISTS data."SchemaInstance"
(
    "SchemaInstanceId" uuid NOT NULL,
    "SchemaId" uuid NOT NULL,
    "SchemaInstanceName" character varying(50) COLLATE pg_catalog."default",
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "SchemaInstanceNamespace" character varying(250) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "SchemaInstance_pkey" PRIMARY KEY ("SchemaInstanceId"),
    CONSTRAINT "SchemaIdMapping" FOREIGN KEY ("SchemaId")
        REFERENCES master."DataSchema" ("SchemaId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS data."SchemaInstance"
    OWNER to postgres;



-- Table: master.SchemaFieldTypes

-- DROP TABLE IF EXISTS master."SchemaFieldTypes";

CREATE TABLE IF NOT EXISTS master."SchemaFieldTypes"
(
    "FieldTypeId" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 100 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "FieldTypeName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "FieldTypeDefaultValue" character varying(50) COLLATE pg_catalog."default",
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone,
    CONSTRAINT "SchemaFieldTypes_pkey" PRIMARY KEY ("FieldTypeId")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."SchemaFieldTypes"
    OWNER to postgres;

-- Table: master.SchemaFields

-- DROP TABLE IF EXISTS master."SchemaFields";

CREATE TABLE IF NOT EXISTS master."SchemaFields"
(
    "SchemaFieldId" uuid NOT NULL,
    "SchemaId" uuid NOT NULL,
    "SchemaFieldName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SchemaFieldTypeKey" integer NOT NULL,
    "SchemaFieldIsRequired" boolean NOT NULL DEFAULT false,
    "SchemaFieldRegex" character varying(150) COLLATE pg_catalog."default",
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    CONSTRAINT "SchemaFields_pkey" PRIMARY KEY ("SchemaFieldId"),
    CONSTRAINT "SchemaFieldsUniqueKey" UNIQUE ("SchemaFieldName", "SchemaId"),
    CONSTRAINT "FieldTypeMapping" FOREIGN KEY ("SchemaFieldTypeKey")
        REFERENCES master."SchemaFieldTypes" ("FieldTypeId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT "SchemaIdMapping" FOREIGN KEY ("SchemaId")
        REFERENCES master."DataSchema" ("SchemaId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master."SchemaFields"
    OWNER to postgres;
-- Table: data.AccessTokens

-- DROP TABLE IF EXISTS data."AccessTokens";

CREATE TABLE IF NOT EXISTS data."AccessTokens"
(
    "AccessTokenId" uuid NOT NULL,
    "AccessScope" character varying(150) COLLATE pg_catalog."default",
    "AccessToken" character varying(500) COLLATE pg_catalog."default",
    "RefreshToken" character varying(500) COLLATE pg_catalog."default" NOT NULL,
    "AccessTokenExpiry" timestamp without time zone NOT NULL,
    "CreatedDate" timestamp without time zone,
    "UpdatedDate" timestamp without time zone,
    "AccessAccountId" uuid NOT NULL,
    CONSTRAINT "AccessTokens_pkey" PRIMARY KEY ("AccessTokenId", "RefreshToken", "AccessTokenExpiry"),
    CONSTRAINT "fk_AccessToken_accessaccount" FOREIGN KEY ("AccessAccountId")
        REFERENCES master."AccessAccounts" ("AccessAccountId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS data."AccessTokens"
    OWNER to postgres;

-- Table: data.SchemaData

-- DROP TABLE IF EXISTS data."SchemaData";

CREATE TABLE IF NOT EXISTS data."SchemaData"
(
    "SchemaDataId" uuid NOT NULL,
    "SchemaInstanceId" uuid NOT NULL,
    "SchemaFieldId" uuid NOT NULL,
    "SchemeDataValue" character varying(1500) COLLATE pg_catalog."default",
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    CONSTRAINT "SchemaData_pkey" PRIMARY KEY ("SchemaDataId"),
    CONSTRAINT "SchemaFieldMapping" FOREIGN KEY ("SchemaFieldId")
        REFERENCES master."SchemaFields" ("SchemaFieldId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "SchemaInstanceMapping" FOREIGN KEY ("SchemaInstanceId")
        REFERENCES data."SchemaInstance" ("SchemaInstanceId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS data."SchemaData"
    OWNER to postgres;


-- Table: data.SettingItems

-- DROP TABLE IF EXISTS data."SettingItems";

CREATE TABLE IF NOT EXISTS data."SettingItems"
(
    "SettingItemId" uuid NOT NULL,
    "SettingItemKey" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "SettingItemNamespace" character varying(250) COLLATE pg_catalog."default" NOT NULL,
    "SettingItemVersion" numeric NOT NULL,
    "SettingItemValue" character varying(500) COLLATE pg_catalog."default",
    "CreatedDate" timestamp without time zone NOT NULL,
    "UpdatedDate" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UpdatedBy" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "SettingItems_pkey" PRIMARY KEY ("SettingItemId")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS data."SettingItems"
    OWNER to postgres;

-- SCHEMA: master

-- DROP SCHEMA IF EXISTS master ;

