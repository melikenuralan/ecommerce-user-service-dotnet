﻿namespace UserService.Application.DTOs
{
    public class GoogleLoginResult
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
