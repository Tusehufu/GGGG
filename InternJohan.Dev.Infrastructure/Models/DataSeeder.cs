//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using InternJohan.Dev.Infrastructure.Models;
//using InternJohan.Dev.Infrastructure;

//public static class DataSeeder
//{
//    public static void Seed(ApplicationDbContext dbContext)
//    {
//        // Skapa en lista över roller
//        var roles = new List<Role>
//        {
//            new Role { Name = "User" },
//            new Role { Name = "Admin" },
//            new Role { Name = "Moderator" }
//        };

//        // Kontrollera och lägg till roller i databasen
//        foreach (var role in roles)
//        {
//            if (!dbContext.Roles.Any(r => r.Name == role.Name))
//            {
//                dbContext.Roles.Add(role);
//            }
//        }

//        // Spara ändringar i databasen
//        dbContext.SaveChanges();
//    }
//}
