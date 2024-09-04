using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Services.Authentication;


public class AuthenticationSeeder
{
    private RoleManager<IdentityRole> roleManager;
    private UserManager<IdentityUser> userManager;
    private readonly IConfiguration _configuration;
    
    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,  IConfiguration configuration)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
        _configuration = configuration;
    }
    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(roleManager);
        tUser.Wait();
    }
    
    private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin")); 
    }
    
    async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("User")); 
    }
    
    public void AddAdmin()
    {
        var tAdmin = CreateAdminIfNotExists();
        tAdmin.Wait();
    }

    private async Task CreateAdminIfNotExists()
    {
        var adminEmail = _configuration["Admin:Email"];
        var adminUsername = _configuration["Admin:Username"];
        var adminPassword = _configuration["Admin:Password"];
        
        
        var adminInDb = await userManager.FindByEmailAsync(adminEmail);
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = adminUsername, Email = adminEmail };
            var adminCreated = await userManager.CreateAsync(admin, adminPassword);

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
