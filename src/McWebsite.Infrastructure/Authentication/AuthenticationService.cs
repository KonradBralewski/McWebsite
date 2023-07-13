using ErrorOr;
using McWebsite.Application.Common.Services;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Common.Errors.SystemUnexpected;
using McWebsite.Domain.User;
using McWebsite.Infrastructure.Exceptions;
using McWebsite.Infrastructure.Persistence;
using McWebsite.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly McWebsiteDbContext _dbContext;
        private readonly UserManager<McWebsiteIdentityUser> _userManager;

        public AuthenticationService(McWebsiteDbContext dbContext, UserManager<McWebsiteIdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<ErrorOr<User>> AddUser(User user)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("BeforeAddingUser");

                _dbContext.DomainUsers.Add(user);
                var addToIdentityTableResult = await _userManager.CreateAsync(new McWebsiteIdentityUser { Email = user.Email.Value }, user.Password.Value);

                int result = await _dbContext.SaveChangesAsync();

                if (!addToIdentityTableResult.Succeeded)
                {
                    if(addToIdentityTableResult.Errors.All(e => e.Code.ToLower().Contains("email") || e.Code.ToLower().Contains("password")))
                    {
                        await transaction.RollbackToSavepointAsync("BeforeAddingUser");
                        return new List<ErrorOr.Error>(addToIdentityTableResult.Errors.Select(e => Error.Validation(e.Code, e.Description)));
                    }
                    
                }

                if (result == 0 || !addToIdentityTableResult.Succeeded)
                {
                    ExceptionsList.ThrowCreationException();
                }

                await transaction.CommitAsync();

                return user;
            }
            catch (Exception ex)
            {
                await transaction.RollbackToSavepointAsync("BeforeAddingUser");
                throw;
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var foundMatchingEmailUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (foundMatchingEmailUser is null)
            {
                return null;
            }

            var user = await _dbContext.DomainUsers.FirstOrDefaultAsync(u => u.Id.Value.ToString() == foundMatchingEmailUser.Id);

            return user;
        }
    }
}
