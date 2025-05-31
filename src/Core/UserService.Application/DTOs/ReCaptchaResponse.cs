using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text.Json.Serialization;

namespace UserService.Application.DTOs
{
    public class ReCaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonPropertyName("hostname")]
        public string HostName { get; set; }

        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
