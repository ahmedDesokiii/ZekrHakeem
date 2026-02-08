namespace ZekrHakeem_v11.Models
{
    public class LessonData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        // Current Save
        public string? Save_FromSorh { get; set; } = default!;
        public int? Save_FromSurah { get; set; } = default!;
        public int? Save_ToSurah { get; set; } = default!;
        public int? Save_FromAyah { get; set; } = default!;
        public string? Save_ToSorh { get; set; } = default!;
        public int? Save_ToAyah { get; set; } = default!;
        public int? Save_Satr { get; set; } = default!;

        // Prev Save
        public string? PrevSave_FromSorh { get; set; } = default!;
        public int? PrevSave_FromAyah { get; set; } = default!;
        public string? PrevSave_ToSorh { get; set; } = default!;
        public int? PrevSave_ToAyah { get; set; } = default!;

        //Rabt
        public string? Rabt_FromSorh { get; set; } = default!;
        public int? Rabt_FromSurah { get; set; } = default!;
        public int? Rabt_ToSurah { get; set; } = default!;
        public int? Rabt_FromAyah { get; set; } = default!;
        public string? Rabt_ToSorh { get; set; } = default!;
        public int? Rabt_ToAyah { get; set; } = default!;
        public int? Rabt_Satr { get; set; } = default!;

        // Rev
        public string? Rev_FromSorh { get; set; } = default!;
        public int? Rev_FromSurah { get; set; } = default!;
        public int? Rev_ToSurah { get; set; } = default!;
        public int? Rev_FromAyah { get; set; } = default!;
        public string? Rev_ToSorh { get; set; } = default!;
        public int? Rev_ToAyah { get; set; } = default!;
        public int? Rev_Satr { get; set; } = default!;

        // Prev Rev
        public string? PrevRev_FromSorh { get; set; } = default!;
        public int? PrevRev_FromAyah { get; set; } = default!;
        public string? PrevRev_ToSorh { get; set; } = default!;
        public int? PrevRev_ToAyah { get; set; } = default!;

        public Guid? LevelDataId { get; set; } = null;
        [ForeignKey("LevelDataId")]
        public LevelData? LevelData { get; set; }


        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<StudentProg> StudentProgs { get; set; } = new List<StudentProg>();

        public int? CurrentState { get; set; }
    }
}
