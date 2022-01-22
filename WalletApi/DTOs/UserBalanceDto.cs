using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class UserBalanceDto
    {
        [JsonPropertyName("user")]
        public string User { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; }
        [JsonPropertyName("Currency")] 
        public string Currency { get; set; }
        [JsonPropertyName("balance")]
        public int Balance { get; set; }
    }
}