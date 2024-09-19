using HandFootLib.Models.DTOs.Login;

namespace HandFootLib.Services.Interfaces;

public interface IUserService
{
    LoginReturnDTO Login(LoginGetDTO loginGetDTO);
}