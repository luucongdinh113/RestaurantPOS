﻿using System;
using System.ComponentModel.DataAnnotations;
namespace RestaurantPOS.Models
{
    public class RegisterViewModel
    {
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Password { get; set; }
        [Required()]
        public string RePassword { get; set; }
        [Required()]
        public string FullName { get; set; }
        [Required()]
        public string PhoneNumber { get; set; }
        [Required()]
        public string Email { get; set; }
        [Required()]
        public bool Gender { get; set; }
        [Required()]
        public DateTime Birthday { get; set; }
    }
}
