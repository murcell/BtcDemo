namespace BtcDemo.Core.Entities
{
	public class CryptoCurrency
	{
        // "id": "90",
        // "symbol": "BTC",
        // "name": "Bitcoin",
        // "nameid": "bitcoin",
        // "rank": 1,
        // "price_usd": "26452.65",
        // "percent_change_24h": "0.18",
        // "percent_change_1h": "-0.24",
        // "percent_change_7d": "0.67",
        // "price_btc": "1.00",
        // "market_cap_usd": "515058196326.62",
        // "volume24": 8511074971.747994,
        // "volume24a": 8585017488.388408,
        // "csupply": "19470946.00",
        // "tsupply": "19470946",
        // "msupply": "21000000"

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
}
