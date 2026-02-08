namespace ZekrHakeem_v11.ViewModels
{
    public class GroupedUserViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public List<UserViewModel> Admins { get; set; }
        public List<UserViewModel> SupervisorsMen { get; set; }
        public List<UserViewModel> SupervisorsWomen { get; set; }
        public List<UserViewModel> SupervisorsCenterMen { get; set; }
        public List<UserViewModel> SupervisorsCenterWomen { get; set; }
        public List<UserViewModel> TeachersMen { get; set; }
        public List<UserViewModel> TeachersWomen { get; set; }
        public List<UserViewModel> Parents { get; set; }



    }
}
