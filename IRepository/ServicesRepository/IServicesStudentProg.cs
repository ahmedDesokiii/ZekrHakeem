namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesStudentProg
    {
        List<StudentProg> GetStudentProgsForMen();
        List<StudentProg> GetStudentProgsForWomen();
        List<StudentProg> GetStudentProgsWithSupervisor(string userid);
        List<StudentProg> GetStudentProgsWithTeacher(string userid);
        List<StudentProg> GetStudentProgsWithParent(ApplicationUser user);
        StudentProg? GetById(Guid id);


    }
}
