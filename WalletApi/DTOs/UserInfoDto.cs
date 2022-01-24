using System.Text.Json.Serialization;

namespace WalletApi.DTOs
{
    public class UserInfoDto
    {
        [JsonPropertyName("user")]
        public string User { get; set; } = default!;
        
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;
        
        [JsonPropertyName("request_uuid")]
        public string RequestUuid { get; set; } = default!;
        
        [JsonPropertyName("country")]
        public string Country { get; set; } = default!;
        
        [JsonPropertyName("birth_date")]
        public string BirthDate { get; set; } = default!;
        
        [JsonPropertyName("registration_date")]
        public string RegistrationDate { get; set; } = default!;
    }
}