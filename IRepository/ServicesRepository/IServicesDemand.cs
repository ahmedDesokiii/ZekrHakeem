namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public interface IServicesDemand
    {
        List<Demand> GetDemandsForMen();
        List<Demand> GetDemandsForWomen();
        List<Demand> GetDemandsWithTeacher(string userid);

        List<Demand> GetDemandsForMen_Notify();
        List<Demand> GetDemandsForWomen_Notify();
        List<Demand> GetDemandsWithTeacher_Notify(string userid);
        bool AddEditPersonalRequest(Demand model);
        bool AddAskRequest(Demand model);
        bool AddComplaintRequest(Demand model);
        bool AddHolidayRequest(Demand model);
        bool AddTransferRequest(Demand model);
        Demand? GetById(Guid id);
        List<Demand> GetStudentDemands(Guid? stid);
        void OpenDemand(Demand model);
        bool AcceptedNewStudent(Guid id);

    }
}
