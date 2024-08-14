namespace login_mvc_jwt.Dto.Users
{
    public class loginResponseDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string email { get; set; } = null!;

        public string token { get; set; } = null!;
    }
}
