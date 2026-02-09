namespace ZekrHakeem_v11.Seeds
{
    public static class DefaultUsers
    {
        //Default Parent
        public static async Task SeedParentUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {

                UserName = "parentuser",
                Email = "parentuser@gmail.com",
                NormalizedUserName = "parentuser".ToUpper(),
                NormalizedEmail = "parentuser@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "parentuser",
                IdentityNum = "p000"


            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                }
            }
        }

        //Default Admin
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "adminuser",
                Email = "adminuser@gmail.com",
                NormalizedUserName = "adminuser".ToUpper(),
                NormalizedEmail = "adminuser@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "adminuser",
                IdentityNum = "a000"

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
                await roleManager.SeedClaimsForAdmin();
            }
        }

        //Default Supervisor Men
        public static async Task SeedSuervisorUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "supervisoruser",
                Email = "supervisoruser@gmail.com",
                NormalizedUserName = "supervisoruser".ToUpper(),
                NormalizedEmail = "supervisoruser@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "supervisoruser",
                IdentityNum = "s000"


            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Teacher.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.SupervisorCenterMen.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Supervisor.ToString());
                }
            }
        }

        //Default Supervisor Women
        public static async Task SeedSuervisorWomenUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superwoman",
                Email = "adesokiii88@gmail.com",
                NormalizedUserName = "superwoman".ToUpper(),
                NormalizedEmail = "adesokiii88@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "superwoman",
                IdentityNum = "sw000"


            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.TeacherWomen.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SupervisorCenterWomen.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SupervisorWomen.ToString());
                }
            }
        }

        //Default Supervisor Center Men
        public static async Task SeedSuervisorCenterMenUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "supervisorcenter",
                Email = "supervisorcenter@gmail.com",
                NormalizedUserName = "supervisorcenter".ToUpper(),
                NormalizedEmail = "supervisorcenter@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "supervisorcenter",
                IdentityNum = "s000"


            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Teacher.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SupervisorCenterMen.ToString());
                }
            }
        }

        //Default Supervisor Center Men
        public static async Task SeedSuervisorCenterWomenUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "supervisorcenterwomen",
                Email = "supervisorcenterwomen@gmail.com",
                NormalizedUserName = "supervisorcenterwomen".ToUpper(),
                NormalizedEmail = "supervisorcenterwomen@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "supervisorcenterwomen",
                IdentityNum = "s000"


            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.TeacherWomen.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SupervisorCenterWomen.ToString());
                }
            }
        }

        //Default Teacher Men
        public static async Task SeedTeacherUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "teacheruser",
                Email = "teacheruser@gmail.com",
                NormalizedUserName = "teacheruser".ToUpper(),
                NormalizedEmail = "teacheruser@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "teacheruser",
                IdentityNum = "t000"

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Teacher.ToString());
                }
            }
        }

        //Default Teacher Women
        public static async Task SeedTeacherWomenUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "teacherwomen",
                Email = "teacherwomen@gmail.com",
                NormalizedUserName = "teacherwomen".ToUpper(),
                NormalizedEmail = "teacherwomen@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "000",
                FullName = "teacherwomen",
                IdentityNum = "t000"

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "$Aa123123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Parent.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.TeacherWomen.ToString());
                }
            }
        }

        private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaim(adminRole, "");
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsList(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}