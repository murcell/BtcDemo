namespace BtcDemo.API.Service.Model
{
	public class CoinloreDto
	{
		public int id { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public string nameid { get; set; }
		public int rank { get; set; }
		public decimal price_usd { get; set; }
		public decimal percent_change_24h { get; set; }
		public decimal percent_change_1h { get; set; }
		public decimal percent_change_7d { get; set; }
		public decimal price_btc { get; set; }
		public decimal market_cap_usd { get; set; }
		public decimal volume24 { get; set; }
		public decimal volume24a { get; set; }
		public decimal csupply { get; set; }
		public decimal tsupply { get; set; }
		public decimal msupply { get; set; }

	}
}
