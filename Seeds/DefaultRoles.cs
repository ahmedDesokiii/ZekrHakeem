namespace ZekrHakeem_v11.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Supervisor.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.SupervisorWomen.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.SupervisorCenterMen.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.SupervisorCenterWomen.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Teacher.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.TeacherWomen.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Parent.ToString()));
            }



        }
    }
}