using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class RequestUserInfoDto
    {
        [JsonPropertyName("user")] 
        public string UserName { get; set; } = default!;
        
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; } = default!;
    }
}