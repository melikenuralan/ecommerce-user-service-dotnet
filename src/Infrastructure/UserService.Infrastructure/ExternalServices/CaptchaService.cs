using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Application.Abstractions;
using UserService.Application.DTOs.Captcha;

namespace UserService.Infrastructure.ExternalServices
{
    public class CaptchaService : ICaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CaptchaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<bool> VerifyTokenAsync(string token)
        {
            var secret = _configuration["ReCaptcha:SecretKey"];

            var response = await _httpClient.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
                null);

            var resultJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Gelen JSON:");
            Console.WriteLine(resultJson);

            var result = JsonSerializer.Deserialize<ReCaptchaResponse>(resultJson);

            if (!result.Success)
            {
                Console.WriteLine("❌ reCAPTCHA başarısız.");
                if (result.ErrorCodes != null)
                    Console.WriteLine("Hatalar: " + string.Join(", ", result.ErrorCodes));
            }

            return result.Success;
        }
    }
}
