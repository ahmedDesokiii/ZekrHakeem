namespace ZekrHakeem_v11.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateCenterPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog",
                $"Permissions.{module}.ViewDetails"
            };
        }
        public static List<string> GenerateProgPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateProgEpisodePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateDistrictPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateLocationPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateEpisodePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.Pause",
                $"Permissions.{module}.Resume",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateStudentPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.DeleteLog",
                //$"Permissions.{module}.ViewDemand",
                //$"Permissions.{module}.CreateDemand",
                //$"Permissions.{module}.EditDemand",
                //$"Permissions.{module}.DeleteDemand"
            };
        }

        public static List<string> GenerateDemandPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"

            };
        }
        public static List<string> GeneratePlanPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateTeacherPermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.DeleteLog"
            };
        }
        public static List<string> GenerateAllPermissions()
        {
            var allCenterPermissions = new List<string>();
            var allProgPermissions = new List<string>();
            var allDistrictPermissions = new List<string>();
            var allLocationPermissions = new List<string>();
            var allEpisodePermissions = new List<string>();
            var allProgEpisodePermissions = new List<string>();
            var allStudentPermissions = new List<string>();
            var allPlanPermissions = new List<string>();
            var allTeacherPermissions = new List<string>();
            var allDemandPermissions = new List<string>();
            var allPermissions = new List<string>();


            allCenterPermissions.AddRange(GenerateCenterPermissionsList(Modules.Centers.ToString()));
            allProgPermissions.AddRange(GenerateCenterPermissionsList(Modules.Progs.ToString()));
            allCenterPermissions.AddRange(GenerateCenterPermissionsList(Modules.Districts.ToString()));
            allLocationPermissions.AddRange(GenerateLocationPermissionsList(Modules.Locations.ToString()));
            allEpisodePermissions.AddRange(GenerateEpisodePermissionsList(Modules.Episodes.ToString()));
            allProgEpisodePermissions.AddRange(GenerateProgEpisodePermissionsList(Modules.ProgEpisodes.ToString()));
            allStudentPermissions.AddRange(GenerateStudentPermissionsList(Modules.Students.ToString()));
            allPlanPermissions.AddRange(GeneratePlanPermissionsList(Modules.Plans.ToString()));
            allPlanPermissions.AddRange(GenerateTeacherPermissionsList(Modules.Teachers.ToString()));
            allEpisodePermissions.AddRange(GenerateDemandPermissionsList(Modules.Demands.ToString()));

            allPermissions = allCenterPermissions.
                                             Concat(allProgPermissions)
                                            .Concat(allDistrictPermissions)
                                            .Concat(allLocationPermissions)
                                            .Concat(allEpisodePermissions)
                                            .Concat(allProgEpisodePermissions)
                                            .Concat(allStudentPermissions)
                                            .Concat(allPlanPermissions)
                                            .Concat(allTeacherPermissions)
                                            .Concat(allDemandPermissions)
                                            .ToList();

            return allPermissions;
        }

        public static class Centers
        {
            public const string View = "Permissions.Centers.View";
            public const string Create = "Permissions.Centers.Create";
            public const string Edit = "Permissions.Centers.Edit";
            public const string Delete = "Permissions.Centers.Delete";
            public const string Pause = "Permissions.Centers.Pause";
            public const string Resume = "Permissions.Centers.Resume";
            public const string DeleteLog = "Permissions.Centers.DeleteLog";
            public const string ViewDetails = "Permissions.Centers.ViewDetails";

        }
        public static class Progs
        {
            public const string View = "Permissions.Progs.View";
            public const string Create = "Permissions.Progs.Create";
            public const string Edit = "Permissions.Progs.Edit";
            public const string Delete = "Permissions.Progs.Delete";
            public const string Pause = "Permissions.Progs.Pause";
            public const string Resume = "Permissions.Progs.Resume";
            public const string DeleteLog = "Permissions.Progs.DeleteLog";

        }

        public static class Districts
        {
            public const string View = "Permissions.Districts.View";
            public const string Create = "Permissions.Districts.Create";
            public const string Edit = "Permissions.Districts.Edit";
            public const string Delete = "Permissions.Districts.Delete";
            //public const string Pause = "Permissions.Districts.Pause";
            public const string DeleteLog = "Permissions.Districts.DeleteLog";

        }

        public static class Locations
        {
            public const string View = "Permissions.Locations.View";
            public const string Create = "Permissions.Locations.Create";
            public const string Edit = "Permissions.Locations.Edit";
            public const string Delete = "Permissions.Locations.Delete";
            public const string Pause = "Permissions.Locations.Pause";
            public const string Resume = "Permissions.Locations.Resume";
            public const string DeleteLog = "Permissions.Locations.DeleteLog";

        }
        public static class Episodes
        {
            public const string View = "Permissions.Episodes.View";
            public const string Create = "Permissions.Episodes.Create";
            public const string Edit = "Permissions.Episodes.Edit";
            public const string Delete = "Permissions.Episodes.Delete";
            public const string Pause = "Permissions.Episodes.Pause";
            public const string Resume = "Permissions.Episodes.Resume";
            public const string DeleteLog = "Permissions.Episodes.DeleteLog";

        }
        public static class ProgEpisodes
        {
            public const string View = "Permissions.ProgEpisodes.View";
            public const string Create = "Permissions.ProgEpisodes.Create";
            public const string Edit = "Permissions.ProgEpisodes.Edit";
            public const string Delete = "Permissions.ProgEpisodes.Delete";
            public const string Pause = "Permissions.ProgEpisodes.Pause";
            public const string Resume = "Permissions.ProgEpisodes.Resume";
            public const string DeleteLog = "Permissions.ProgEpisodes.DeleteLog";

        }
        public static class Students
        {
            public const string View = "Permissions.Students.View";
            public const string Create = "Permissions.Students.Create";
            public const string Edit = "Permissions.Students.Edit";
            public const string Delete = "Permissions.Students.Delete";
            public const string DeleteLog = "Permissions.Students.DeleteLog";
        }
        public static class Demands
        {
            public const string View = "Permissions.Demands.View";
            public const string Create = "Permissions.Demands.Create";
            public const string Edit = "Permissions.Demands.Edit";
            public const string Delete = "Permissions.Demands.Delete";
            public const string DeleteLog = "Permissions.Demands.DeleteLog";

        }
        public static class Plans
        {
            public const string View = "Permissions.Plans.View";
            public const string Create = "Permissions.Plans.Create";
            public const string Edit = "Permissions.Plans.Edit";
            public const string Delete = "Permissions.Plans.Delete";
            public const string DeleteLog = "Permissions.Plans.DeleteLog";
        }
        public static class Teachers
        {
            public const string View = "Permissions.Teachers.View";
            public const string Create = "Permissions.Teachers.Create";
            public const string Edit = "Permissions.Teachers.Edit";
            public const string Delete = "Permissions.Teachers.Delete";
            public const string DeleteLog = "Permissions.Teachers.DeleteLog";

        }
    }
}