﻿using Microsoft.AspNetCore.Mvc;
using VinayakAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VinayakAPI.Interfaces;

namespace VinayakAPI.Repository
{
    public class LoginRepository:ILogin
    {
        //public IActionResult Login(LoginModel loginModel)
        //{
        //    // For demo, validate username/password
        //    if (loginModel.Username != "testuser" || loginModel.Password != "password");
        //    return "";
        //}
    }
}
