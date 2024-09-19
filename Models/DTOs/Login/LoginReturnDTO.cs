namespace HandFootLib.Models.DTOs.Login
{
    public class LoginReturnDTO
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? NickName { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
}