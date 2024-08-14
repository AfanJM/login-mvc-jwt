using login_mvc_jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace login_mvc_jwt.Context
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }

       public  DbSet<UsersModels> Users { get; set; }


    }
}
