namespace ZekrHakeem_v11.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Center>().Property(b => b.ApplicationUserId).IsRequired(false);
            builder.Entity<Prog>().Property(b => b.ApplicationUserId).IsRequired(false);
            builder.Entity<Episode>().Property(b => b.ApplicationUserId).IsRequired(false);
            builder.Entity<StudentProg>().Property(e => e.StEpisodeProgId).IsRequired(false);
            builder.Entity<Attendance>().Property(e => e.EpisodeProgId).IsRequired(false);
            builder.Entity<Attendance>().Property(e => e.EpisodeId).IsRequired(false);
            builder.Entity<Demand>().Property(e => e.ApplicationUserId).IsRequired(false);

            builder.Entity<Center>()
               .HasData(new Center
               {
                   Id = new Guid("00000000-0000-0000-0000-000000000001"),
                   Name = "C_RemoteM",
                   Type = "men",
                   CurrentState = 0,
                   ApplicationUserId = null,
                   PauseState = 0,
                   Description = null
               });
            builder.Entity<Center>()
              .HasData(new Center
              {
                  Id = new Guid("00000000-0000-0000-0000-000000000004"),
                  Name = "C_RemoteW",
                  Type = "women",
                  CurrentState = 0,
                  ApplicationUserId = null,
                  PauseState = 0,
                  Description = null
              });
            builder.Entity<District>()
              .HasData(new District
              {
                  Id = new Guid("00000000-0000-0000-0000-000000000002"),
                  Name = "D_Remote",
                  //CenterId = new Guid("00000000-0000-0000-0000-000000000001"),
                  CurrentState = 0,
                  Description = null
              });
            builder.Entity<Location>()
              .HasData(new Location
              {
                  Id = new Guid("00000000-0000-0000-0000-000000000003"),
                  Name = "L_RemoteM",
                  Type = "mosque",
                  LoCenterId = new Guid("00000000-0000-0000-0000-000000000001"),
                  DistrictId = new Guid("00000000-0000-0000-0000-000000000002"),
                  CurrentState = 0,
                  Latitude = "25.043773164267765",
                  Longitude = "37.25986633254091",
                  Description = null,
                  TotalStudents = "0",
                  PauseState = 0
              });
            builder.Entity<Location>()
              .HasData(new Location
              {
                  Id = new Guid("00000000-0000-0000-0000-000000000005"),
                  Name = "L_RemoteW",
                  Type = "dar",
                  LoCenterId = new Guid("00000000-0000-0000-0000-000000000004"),
                  DistrictId = new Guid("00000000-0000-0000-0000-000000000002"),
                  CurrentState = 0,
                  Latitude = "25.043773164267765",
                  Longitude = "37.25986633254091",
                  Description = null,
                  TotalStudents = "0",
                  PauseState = 0
              });


            //builder.Entity<LevelData>()
            //   .HasData(new LevelData
            //   {
            //       Id = new Guid("00000000-0000-0000-0000-000000000101"),
            //       Name = "بدون مستوي",
            //       Num = 0,
            //       CurrentState = 0,
            //       //PlanDataId = Guid.Parse("c611b8c8-184a-4fa0-8852-b60ba5cf5635"),//خطة عامة
            //       PlanDataId = Guid.Parse("5b35a4de-5100-4f4b-b065-527f04ae1472"),//خطة عامة web
            //       Description = null

            //   });

            builder.Entity<Center>().Property(c => c.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogCenter>().Property(lc => lc.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Prog>().Property(c => c.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogProg>().Property(lc => lc.Id).HasDefaultValueSql("(newid())");

            builder.Entity<District>().Property(lc => lc.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Location>().Property(l => l.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogLocation>().Property(ll => ll.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Episode>().Property(ep => ep.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogEpisode>().Property(lep => lep.Id).HasDefaultValueSql("(newid())");

            builder.Entity<EpisodeProg>().Property(ep => ep.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogEpisodeProg>().Property(lep => lep.Id).HasDefaultValueSql("(newid())");

            builder.Entity<EpisodeType>().Property(ep => ep.Id).HasDefaultValueSql("(newid())");
            builder.Entity<Nationality>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<HealthCondition>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<PlanData>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogPlanData>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<LevelData>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogLevelData>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<StudentProg>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogStudentProg>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogStudent>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<EvaluationProg>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<Evaluation>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogEvaluation>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Demand>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<LogDemand>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<PrayerTime>().Property(x => x.Id).HasDefaultValueSql("(newid())");



            base.OnModelCreating(builder);

            // ===== Users / Roles / Permissions (DB driven) =====
            builder.Entity<ApplicationUser>().Property(u => u.CreatedAt).HasDefaultValueSql("(getutcdate())");

            builder.Entity<AppPermission>()
                .HasOne(p => p.Module).WithMany(m => m.Permissions)
                .HasForeignKey(p => p.ModuleId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role).WithMany()
                .HasForeignKey(rp => rp.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserPermission>()
                .HasOne(up => up.User).WithMany()
                .HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserPermission>()
                .HasOne(up => up.Permission).WithMany(p => p.UserPermissions)
                .HasForeignKey(up => up.PermissionId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RoleInfo>()
                .HasOne(ri => ri.Role).WithOne()
                .HasForeignKey<RoleInfo>(ri => ri.RoleId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserActivity>().Property(a => a.CreatedAt).HasDefaultValueSql("(getutcdate())");
        }
        public DbSet<Center> Centers { get; set; }
        public DbSet<LogCenter> LogCenters { get; set; }
        public DbSet<Prog> Progs { get; set; }
        public DbSet<LogProg> LogProgs { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LogLocation> LogLocations { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<LogEpisode> LogEpisodes { get; set; }

        public DbSet<EpisodeProg> EpisodeProgs { get; set; }
        public DbSet<LogEpisodeProg> LogEpisodeProgs { get; set; }

        public DbSet<EpisodeType> EpisodesTypes { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<HealthCondition> HealthConditions { get; set; }

        public DbSet<PlanData> PlansData { get; set; }
        public DbSet<LogPlanData> LogPlansData { get; set; }
        public DbSet<LevelData> LevelsData { get; set; }
        public DbSet<LogLevelData> LogLevelsData { get; set; }


        public DbSet<Student> Students { get; set; }
        public DbSet<LogStudent> LogStudents { get; set; }

        public DbSet<StudentProg> StudentProgs { get; set; }
        public DbSet<LogStudentProg> LogStudentProgs { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<LogEvaluation> LogEvaluations { get; set; }
        public DbSet<EvaluationProg> EvaluationProgs { get; set; }
        public DbSet<LessonData> LessonsData { get; set; }
        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Verse> Verses { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<LogDemand> LogDemands { get; set; }
        public DbSet<PrayerTime> PrayerTimes { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        // Security
        public DbSet<AppModule> AppModules { get; set; }
        public DbSet<AppPermission> AppPermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<LogAttendance> LogAttendances { get; set; }
    }
}
