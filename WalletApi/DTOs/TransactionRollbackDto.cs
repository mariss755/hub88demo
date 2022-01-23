using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class TransactionRollbackDto
    {
        [JsonPropertyName("user")]
        public string UserName { get; set; }
        [JsonPropertyName("transaction_uuid")]
        public string TransactionUuid { get; set; }
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; }
        [JsonPropertyName("reference_transaction_uuid")]
        public string ReferenceTransationUuid { get; set; }
    }
}