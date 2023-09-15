using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.Utilities.ValidaitonError;

namespace BtcDemo.Core.Utilities.Results;

public interface IResult
{
	public ResultStatus ResultStatus { get; }
	public string Message { get; }
	public Exception Exception { get; }
	public IEnumerable<ValidationError> ValidationErrors { get; set; }

}
