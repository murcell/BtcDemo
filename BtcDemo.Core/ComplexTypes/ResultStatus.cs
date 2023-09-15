namespace BtcDemo.Core.ComplexTypes
{
	public enum ResultStatus
	{
		Success = 0,
		Error = 1,
		Warning = 2,
		Info = 3,

		Forbidden = 403,
		UnAuthorized = 401,
		ServerError = 500

	}
}
