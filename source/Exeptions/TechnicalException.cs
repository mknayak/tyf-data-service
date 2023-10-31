using tyf.data.service.Extensions;

namespace tyf.data.service.Exeptions
{
    public class TechnicalException:Exception
	{
		public string ErrorCode { get; set; }
		public TechnicalException(string errorCode,string errorMessage):base(errorMessage)
		{
			ErrorCode = errorCode;
		}
        public TechnicalException(ErrorMessage msg) : base(msg.Message)
        {
            ErrorCode = msg.Key;
        }
    }
}

