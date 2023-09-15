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
}
