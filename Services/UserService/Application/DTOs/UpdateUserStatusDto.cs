﻿namespace UserService.Application.DTOs
{
    public class UpdateUserStatusDto
    {
        public bool IsActive { get; set; }
        public bool PhoneVerified { get; set; }
    }
}
