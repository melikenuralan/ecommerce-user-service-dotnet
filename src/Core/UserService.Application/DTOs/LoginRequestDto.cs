﻿namespace UserService.Application.DTOs
{
    public class LoginRequestDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
