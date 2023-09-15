namespace BtcDemo.Core.Utilities;

public static class Messages
{

	public static class General
	{
		public static string ValidationError()
		{
			return "Bir veya daha fazla validasyon hatası ile karşılaşıldı.";
		}

		public static string Unauthorized()
		{
			return $"Token yok ya da hatalı";
		}

		public static string UnexpectedError()
		{
			return "Beklenmeyen bir hata ile karşılaşıldı.";
		}
	}

	public static class TokenMessage
	{
		public static string UnexpectedError()
		{
			return "Beklenmeyen bir hata ile karşılaşıldı.";
		}

	}

	public static class CoinMessage
	{
		public static string NotFound(bool isPlural)
		{
			if (isPlural)
				return "Herhangi bir coin kaydı bulunamadı.";
			return "Böyle bir coin bulunamadı.";
		}

		public static string Found(bool isPlural)
		{
			if (isPlural)
				return "Coin veritabanından başarıyla getirildi.";
			return "Coin veritabanından başarıyla getirildi.";
		}

		public static string Add()
		{
			return $"Coin başarıyla eklenmiştir.";
		}

		
		
		public static string Delete(string id)
		{
			return $"{id} li coin  başarıyla silinmiştir.";
		}

		public static string UnexpectedError()
		{
			return "Beklenmeyen bir hata ile karşılaşıldı.";
		}

	}
}
