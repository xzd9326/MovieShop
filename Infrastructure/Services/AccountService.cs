using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResponseModel> LoginUser(string email, string password)
        {
            // go to databse and get the user row by email
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new Exception("Email does not exist");
            }

            var hashedPassword = GetHashedPassword(password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                // password is good
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault()
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<bool> RegisterUser(UserRegisterModel model)
        {
            // go to database and check if the email exists already
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser != null)
            {
                // user already exists
                throw new ConflictException("Email already exists");
            }

            // create a random salt

            var salt = GetRandomSalt();

            // create hashed password with the salt created above
            // Never ever create your own hashing algorithms
            // Pdbkf2 (Microsoft), Bcrypt, Aargon2
            var hashedPassword = GetHashedPassword(model.Password, salt);

            var user = new User
            {
                Email = model.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            var createdUser = await _userRepository.Add(user);
            // save the user object to User table
            if (createdUser.Id > 0)
            {
                return true;
            }
            return false;
        }

        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password,
               Convert.FromBase64String(salt),
               KeyDerivationPrf.HMACSHA512,
               10000,
               256 / 8));
            return hashed;
        }
    }
}
