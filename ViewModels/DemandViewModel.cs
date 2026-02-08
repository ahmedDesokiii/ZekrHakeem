namespace ZekrHakeem_v11.ViewModels
{
    public class DemandViewModel
    {
        public List<Demand>? Demands { get; set; }
        public List<Demand>? DemandsForMen { get; set; }
        public List<Demand>? DemandsForWomen { get; set; }
        public List<Demand>? DemandsForTeacher { get; set; }
        public List<Demand>? DemandsForMen_Notify { get; set; }
        public List<Demand>? DemandsForWomen_Notify { get; set; }
        public List<Demand>? DemandsForTeacher_Notify { get; set; }
        public LevelViewModel Levels { get; set; }
        public List<LogDemand>? LogDemands { get; set; }
        public Demand? NewDemand { get; set; }
        public List<Demand> SelectStudentDemands { get; set; }


        //public LoginModel LoginModel { get; set; }

    }
}
