using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class UserBalanceDto
    {
        [JsonPropertyName("user")]
        public string User { get; set; } = default!;
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; } = default!;
        [JsonPropertyName("Currency")] 
        public string Currency { get; set; } = default!;
        [JsonPropertyName("balance")]
        public int Balance { get; set; } = default!;
    }
}