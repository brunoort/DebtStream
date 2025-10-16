using System.Text.Json.Serialization;

namespace DevTest.Service.Models
{
    public class RegisterViewModel
    {
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

    }
}

