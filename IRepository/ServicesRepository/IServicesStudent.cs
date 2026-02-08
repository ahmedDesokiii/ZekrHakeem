namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesStudent
    {
        List<Student> GetStudentsForMen();
        List<Student> GetStudentsForWomen();
        List<Student> GetStudentsWithSupervisor(string userid);
        List<Student> GetStudentsWithTeacher(string userid);
        List<Student> GetStudentsWithParent(ApplicationUser user);
        List<Evaluation> GetEvaluationsForStudent(Guid stId);
        List<Demand> GetDemandsForStudent(Guid stid);
        bool FindByLimit(int episodeLimit, int episodeStudents);
        Student? GetById(Guid id);

        List<Nationality> GetNationalities();
        Nationality FindByNationalityName(string Name);
        Nationality FindByNationalityId(Guid Id);
        bool SaveNationality(Nationality model);
        bool DeletNationality(Guid Id);
        List<HealthCondition> GetHealth();
        HealthCondition FindByHealthName(string Name);
        HealthCondition FindByHealthId(Guid Id);
        bool SaveHealth(HealthCondition model);
        bool DeleteHealth(Guid Id);
        LevelData? GetSelectedLevel();
        //bool SaveST(StudentViewModel model);
        bool AddNewStudentRequest(Student model);
        bool WaitEpisodes(Student model);
        bool Assigned(Student model);

    }
}
