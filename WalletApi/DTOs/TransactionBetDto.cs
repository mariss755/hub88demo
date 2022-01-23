using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class TransactionBetDto
    {
        [JsonPropertyName("user")]
        public string UserName { get; set; } = default!;
        [JsonPropertyName("transaction_uuid")]
        public string TransactionUuid { get; set; } = default!;
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; } = default!;
        [JsonPropertyName("currency")] 
        public string Currency { get; set; } = default!;
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
    }
}