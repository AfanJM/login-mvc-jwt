namespace login_mvc_jwt.Models
{
    public class UsersModels
    {


        public int  Id { get; set; }

        public string UserName { get; set; } = null!;

        public string email { get; set; } = null!;

        public string password { get; set; } = null!;



    }
}
