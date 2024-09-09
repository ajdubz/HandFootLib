﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HandFootLib.Services;

public class UserService : IUserService
{
    private readonly IPlayerService _playerService;

    public UserService(IPlayerService playerService) => _playerService = playerService;


    public LoginReturnDTO Login(LoginGetDTO loginGetDTO)
    {

        try
        {
            var isValid = ValidatePlayer(loginGetDTO);

            if (!isValid)
            {
                Console.WriteLine("Invalid player credentials");
                throw new UnauthorizedAccessException("Invalid player credentials");
            }


            var key = Encoding.ASCII.GetBytes("ThisIsASampleKeyThatIsTotallyArbitraryAsLongAsItMatchesItsPairKeyInOtherFileAndCredIsSymmetricSecurityKey");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginGetDTO.NickName ?? ""),
                    new Claim(ClaimTypes.Name, loginGetDTO.NickName ?? ""),
                    new Claim(ClaimTypes.Email, loginGetDTO.Email ?? ""),
                    //new Claim("customProp", "testCustom")
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var writeToken = tokenHandler.WriteToken(token);

            return new LoginReturnDTO { NickName = loginGetDTO.NickName, Email = loginGetDTO.Email, Token = writeToken, };
        }
        catch (Exception ex)
        {
            // Handle the exception here
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; // Rethrow the exception to propagate it further if needed
        }

    }

    private bool ValidatePlayer(LoginGetDTO loginGetDTO)
    {
        try
        {
            var player = _playerService.GetPlayers().Select(x => new { x.NickName, x.Email, x.Password })
                .SingleOrDefault(p => (p.NickName == loginGetDTO.NickName || p.Email == loginGetDTO.Email) && p.Password == loginGetDTO.Password);

            return player != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}