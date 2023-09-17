namespace BtcDemo.Client.Models;

public class CoinModel
{
	public long Id { get; set; }
	public string Symbol { get; set; }
    public string Name { get; set; }
    public int Rank { get; set; }
    public decimal PriceUsd { get; set; }
    public decimal PercentChange24h { get; set; }
    public decimal PercentChange1h { get; set; }
    public decimal PercentChange7d { get; set; }
    public decimal CSupply { get; set; }
    public decimal TSupply { get; set; }
    public decimal MSupply { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedDateStr { get; set; }
}

public class CoinResponseModel
{
	public CoinResponseModel()
	{
		//Data = new List<CoinModel>();
	}

	public List<CoinModel>? Data { get; set; }
	public int ResultStatus { get; set; }
	public string Message { get; set; }
	public object Exception { get; set; }
	public object ValidationErrors { get; set; }
}

