using System;
using System.Text.Json.Serialization;
using WalletApi.Entities;

namespace WalletApi.DTOs
{
    public class UserInfoDto
    {
        [JsonPropertyName("user")]
        public string User { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("birth_date")]
        public string BirthDate { get; set; }
        [JsonPropertyName("registration_date")]
        public string RegistrationDate { get; set; }
    }
}