namespace BtcDemo.Client.Models;

public class JwtTokenResponseModel
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}


public class JwtResponse
{
    public JwtResponse()
    {
        Data = new JwtTokenResponseModel();    
    }

    public JwtTokenResponseModel Data { get; set; }
    public int ResultStatus { get; set; }
    public string Message { get; set; }
    public object Exception { get; set; }
    public object ValidationErrors { get; set; }
}
