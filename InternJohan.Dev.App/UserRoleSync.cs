//using InternJohan.Dev.App;
//using InternJohan.Dev.Infrastructure.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Threading.Tasks;

//namespace InternJohan.Dev.App
//{
//    public class UserRoleSync
//    {
//        private readonly IConfiguration _configuration;

//        public UserRoleSync(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public async Task SyncUsersAndRolesAsync()
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

//            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
//            {
//                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int, IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>(dbContext), null, null, null, null, null, null, null, null);
//                var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole, ApplicationDbContext, int, IdentityUserRole<int>, IdentityRoleClaim<int>>(dbContext), null, null, null, null);

//                // Synkronisera roller
//                await SyncRolesAsync(roleManager);

//                // Synkronisera användare
//                await SyncUsersAsync(userManager, dbContext);
//            }
//        }

//        private async Task SyncRolesAsync(RoleManager<ApplicationRole> roleManager)
//        {
//            // Hämta befintliga roller från din befintliga databas
//            var existingRoles = await _dbContext.Roles.ToListAsync();

//            foreach (var existingRole in existingRoles)
//            {
//                // Kontrollera om rollen redan finns i Identity Framework
//                if (!await roleManager.RoleExistsAsync(existingRole.Name))
//                {
//                    // Om rollen inte finns, lägg till den
//                    var newRole = new ApplicationRole { Name = existingRole.Name };
//                    await roleManager.CreateAsync(newRole);
//                }
//            }
//        }

//        private async Task SyncUsersAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
//        {
//            // Hämta befintliga användare från din befintliga databas
//            var existingUsers = await _dbContext.Users.ToListAsync();

//            foreach (var existingUser in existingUsers)
//            {
//                // Kontrollera om användaren redan finns i Identity Framework
//                if (await userManager.FindByNameAsync(existingUser.Username) == null)
//                {
//                    // Om användaren inte finns, lägg till den
//                    var newUser = new ApplicationUser { UserName = existingUser.Username, Email = existingUser.Email };
//                    await userManager.CreateAsync(newUser, existingUser.Password);
//                }
//            }
//        }

//    }
//}
