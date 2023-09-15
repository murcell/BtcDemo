namespace BtcDemo.Core.DTOs;

public class CoinDto
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
}
