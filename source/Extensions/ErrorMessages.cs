using System;
namespace tyf.data.service.Extensions
{
	public class ErrorMessages:Dictionary<string,string>
	{
		public const string Key = "ErrorMessages";

		public ErrorMessage Format(string key, params string[] fields)
		{
			var msgFormat = this.ContainsKey(key) ? this[key] : string.Empty;
			var message= string.Format(msgFormat, fields);
			return new ErrorMessage { Key = key, Message = message };
		}
	}

	public class ErrorMessage
	{
		public required string Key { get; set; }
		public string? Message { get; set; }
	}

	public class SecurityOption
	{
		public const string Key = "Security";
		public string SignKey { get; set; }
	}
}

