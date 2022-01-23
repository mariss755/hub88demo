﻿using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class TransactionRollbackDto
    {
        [JsonPropertyName("user")]
        public string UserName { get; set; } = default!;
        [JsonPropertyName("transaction_uuid")]
        public string TransactionUuid { get; set; } = default!;
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; } = default!;
        [JsonPropertyName("reference_transaction_uuid")]
        public string ReferenceTransationUuid { get; set; } = default!;
    }
}