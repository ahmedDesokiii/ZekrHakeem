namespace ZekrHakeem_v11.Models
{
    //public class WhatsAppMessage
    //{
    //    //Message Data
    //    public string? InstanceId { get; set; }
    //    public string? Token { get; set; }
    //    public string? Client { get; set; }
    //    public string? Message { get; set; }
    //    public string? ImageUrl { get; set; }

    //    //--------------------------------------
    //    //Date Data
    //    public DateTime _currentDate { get; set; } = DateTime.Now.Date;
    //    public string _currentDay { get; set; } = DateTime.Now.DayOfWeek.ToString();

    //    //Student Data

    //    public string stName { get; set; }
    //    public string stEpName { get; set; }
    //    public string stEpPeriod { get; set; }
    //    public string stEvAttend { get; set; }

    //    //---------------------------------------

    //    public string Save_FromSurah { get; set; }
    //    public string Save_FromAyah { get; set; }
    //    public string Save_ToSurah { get; set; }
    //    public string Save_ToAyah { get; set; }
    //    public string Save_Ev { get; set; }

    //    public string Rabt_FromSurah { get; set; }
    //    public string Rabt_FromAyah { get; set; }
    //    public string Rabt_ToSurah { get; set; }
    //    public string Rabt_ToAyah { get; set; }
    //    public string Rabt_Ev { get; set; }

    //    public string Rev_FromSurah { get; set; }
    //    public string Rev_FromAyah { get; set; }
    //    public string Rev_ToSurah { get; set; }
    //    public string Rev_ToAyah { get; set; }
    //    public string Rev_Ev { get; set; }



    //}
    public class WhatsAppMessage
    {
        public string? InstanceId { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public string? ImageUrl { get; set; }

        public string episodeId { get; set; }
        // الحقول الجديدة للنصوص الأربعة
        public string? SurahName1 { get; set; }
        public string? AyahText1 { get; set; }
        public string? SurahName2 { get; set; }
        public string? AyahText2 { get; set; }
    }

    public class WhatsAppResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? MessageId { get; set; }
    }
}
