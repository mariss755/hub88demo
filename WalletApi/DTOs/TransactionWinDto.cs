using System.Text.Json.Serialization;
using WalletApi.Enums;

namespace WalletApi.DTOs
{
    public class TransactionWinDto
    {
        [JsonPropertyName("user")]
        public string UserName { get; set; }
        [JsonPropertyName("transaction_uuid")]
        public string TransactionUuid { get; set; }
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; }
        [JsonPropertyName("reference_transaction_uuid")]
        public string ReferenceTransationUuid { get; set; }
        [JsonPropertyName("currency")] 
        public Currency Currency { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        
    }
}