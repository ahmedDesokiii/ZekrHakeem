namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesTeacher : IServicesTeacher
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServicesTeacher(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region AllTeachers
        //public List<UserViewModel> GetAllTeachers()
        //{
        //    try
        //    {
        //        //var allusers = _context.Users.Select(user => new UserViewModel()
        //        //{
        //        //    Id = user.Id,
        //        //    FullName = user.FullName,
        //        //    IdentityNum = user.IdentityNum,
        //        //    PhoneNumber = user.PhoneNumber,
        //        //    UserName = user.UserName,
        //        //    Email = user.Email,
        //        //}).ToList();
        //        //var teachersmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
        //        //var teacherswomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();

        //    //    var _teachers = _context.Users
        //    //                //.Include(x => x.Attendances)
        //    //                //.Include(x => x.Episodes)
        //    //                //.OrderBy(x => x.Attendances).ThenBy(x => x.FullName)
        //    //                .Select(user => new UserViewModel()
        //    //                {
        //    //                    Id = user.Id,
        //    //                    FullName = user.FullName,
        //    //                    IdentityNum = user.IdentityNum,
        //    //                    PhoneNumber = user.PhoneNumber,
        //    //                    UserName = user.UserName,
        //    //                    Email = user.Email,
        //    //                })
        //    //                .Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString()))
        //    //                .ToList();


        //    //    return _teachers;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return null;
        //    //}
        //}
        #endregion
    }
}
