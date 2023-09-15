namespace BtcDemo.Core.Utilities.Results;

public interface IDataResult<out T> : IResult // out T olarak kullanırsak hem liste hem de object gönderebiliyoruz.
{
	public T Data { get; }
}
