using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.Utilities.ValidaitonError;

namespace BtcDemo.Core.Utilities.Results;

public class DataResult<T> : IDataResult<T>
{
	public DataResult(ResultStatus resultStatus, T data)
	{
		ResultStatus = resultStatus;
		Data = data;
	}

	public DataResult(ResultStatus resultStatus, T data, IEnumerable<ValidationError> validationErrors)
	{
		ResultStatus = resultStatus;
		Data = data;
		ValidationErrors = validationErrors;
	}

	public DataResult(ResultStatus resultStatus, string message, T data)
	{
		ResultStatus = resultStatus;
		Data = data;
		Message = message;
	}

	public DataResult(ResultStatus resultStatus, string message, T data, IEnumerable<ValidationError> validationErrors)
	{
		ResultStatus = resultStatus;
		Data = data;
		Message = message;
		ValidationErrors = validationErrors;
	}

	public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
	{
		ResultStatus = resultStatus;
		Data = data;
		Message = message;
		Exception = exception;
	}

	public DataResult(ResultStatus resultStatus, string message, T data, Exception exception, IEnumerable<ValidationError> validationErrors)
	{
		ResultStatus = resultStatus;
		Data = data;
		Message = message;
		Exception = exception;
		ValidationErrors = validationErrors;
	}
	public T Data { get; }

	public ResultStatus ResultStatus { get; }

	public string Message { get; }

	public Exception Exception { get; }
	public IEnumerable<ValidationError> ValidationErrors { get; set; }

}
