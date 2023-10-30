using System;
namespace tyf.data.service
{
	public static class Constants
	{
		public static class ErrorCodes
		{
			public static class Repository
			{
				public const string DuplicateEntity = "CER-101";
				public const string EntityNotFound = "CER-102";
            }
		}
		public static class ConfigKeys
		{
			public const string SystemInformation = nameof(SystemInformation);
		}
        public static class Roles
        {
            public const string Administrator = nameof(Administrator);
            public const string DataManager = nameof(DataManager);
            public const string UserManager = nameof(UserManager);
        }
    }
}

