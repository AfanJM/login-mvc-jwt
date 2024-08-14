using AutoMapper;
using BCrypt.Net;
using login_mvc_jwt.Context;
using login_mvc_jwt.Dto.Users;
using login_mvc_jwt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace login_mvc_jwt.repository.Users
{
    public class RepositoryUsers : IRepositoryUsers
    {
        private readonly IMapper _mapper;

        private readonly AppDbContext _context;
        public RepositoryUsers(IMapper mapper, AppDbContext context)
        {
            _context = context;

            _mapper = mapper;
        }

        public async Task<UsersModels> register(registerDto registerDto)
        {
            //mapear el dto al modelo
            var users = _mapper.Map<UsersModels>(registerDto);

            users.password = BCrypt.Net.BCrypt.HashPassword(registerDto.password);

            await _context.AddAsync(users);

            await _context.SaveChangesAsync();

            return users;



        }

        public async Task<UsersModels> login(loginDto loginDto)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == loginDto.email);

            if (user == null)
            {
                throw new Exception("The user not found");
            }

            bool isCheckPassword = BCrypt.Net.BCrypt.Verify(loginDto.password, user.password);

            if (!isCheckPassword)
            {

                throw new Exception("Password is not valid");
            }

            return user;


        }

        

    }


}
