using AssessmentAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssessmentAPI.Data.Initialize
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;

        public DbInitializer(

            ApplicationDbContext db)
        {
            _db = db;
        }


        public void Initialize(bool e)
        {

            //migrations if they are not applied
            try
            {
                if (e)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }


            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }




            ////create roles if they are not created
            //if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            //{
            //    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();


            //    //if roles are not created, then we will create admin user as well
            //    _userManager.CreateAsync(new ApplicationUser
            //    {
            //        UserName = "admin@dotnetmastery.com",
            //        Email = "admin@dotnetmastery.com",
            //        Name = "Bhrugen Patel",
            //        PhoneNumber = "1112223333",
            //        StreetAddress = "test 123 Ave",
            //        State = "IL",
            //        PostalCode = "23422",
            //        City = "Chicago"
            //    }, "Admin123*").GetAwaiter().GetResult();


            //    ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");
            //    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            //}

            return;
        }
    }
}
