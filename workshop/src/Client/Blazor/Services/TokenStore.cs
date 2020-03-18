namespace Client.Services
{
    public class TokenStore
        : ITokenStore
    {
        private string _token;
        
        public string GetToken()
        {
            return _token;
        }

        public void SetToken(string token)
        {
            _token = token;
        }
    }
}