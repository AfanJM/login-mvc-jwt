using AutoMapper;
using BCrypt.Net;
using login_mvc_jwt.Context;
using login_mvc_jwt.Dto.Users;
using login_mvc_jwt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace login_mvc_jwt.repository.Users
{
    public class RepositoryUsers : IRepositoryUsers
    {
        private readonly IMapper _mapper;

        private readonly AppDbContext _context;

        private readonly IConfiguration _config;
        public RepositoryUsers(IMapper mapper, AppDbContext context, IConfiguration config)
        {
            _context = context;

            _mapper = mapper;

            _config = config;
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

        public async Task<loginResponseDto> login(loginDto loginDto)
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

            //generamos jwt
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]

                {
            new Claim(ClaimTypes.Name, user.Id.ToString()),

            new Claim(ClaimTypes.Email, user.email),

            new Claim(ClaimTypes.Name, user.UserName)

                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),

                Issuer = _config["Jwt:Issuer"],

                Audience = _config["Jwt:Audience"],

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return new loginResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                email = user.email,
                token = tokenString,

            };

        }

    }


}
