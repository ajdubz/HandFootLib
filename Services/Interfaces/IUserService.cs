using HandFootLib.Models.DTOs;

namespace HandFootLib.Services.Interfaces;

public interface IUserService
{
    LoginReturnDTO Login(LoginGetDTO loginGetDTO);
}