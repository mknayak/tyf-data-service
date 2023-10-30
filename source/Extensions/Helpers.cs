using System;
namespace tyf.data.service.Extensions
{
	public static class Helpers
	{
		public static string ValidNamespace(string input)
		{
			var lowerStr = input.ToLowerInvariant();
			var onlyString = lowerStr.Where(x => char.IsLetter(x)).ToArray();
			return new string(onlyString);
		}
		 
	}
}

