using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.ServiceLayer;
using Backend.Models;
using Backend.Data.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;//esta
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;



namespace Backend.ControllersLog
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ServiceUsuario _service;

        private IConfiguration config;
        public UsuarioController (ServiceUsuario service,IConfiguration config)
        {
            _service = service;
            this.config= config;
        }
    
        private string GenerateToken(string Ci,string Rol)
        {
            var claims =new []{
                new Claim(ClaimTypes.NameIdentifier,Ci),
                new Claim("AdminType",Rol)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JMT:Key").Value));//dudado
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(120),
                                    signingCredentials:creds
                                    ); 
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token; 
        }

            
    }
}
