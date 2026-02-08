namespace ZekrHakeem_v11.Constants
{
    public class Helper
    {
        public const string PathImageuser = "/Images/Users/";
        public const string PathSaveImageuser = "Images/Users";


        public const string Success = "success";
        public const string Error = "error";
        public const string Warning = "warning";

        public const string MsgType = "msgType";
        public const string Title = "title";
        public const string Msg = "msg";

        public const string AddRequestAsk = "New Request Ask";
        public const string Save = "Add New";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string Pause = "Pause";
        public const string Resume = "Resume";

        public enum eCurrentState
        {
            Active = 1,
            Delete = 0
        }
        public enum ePauseState
        {
            Active = 1,
            Pause = 0
        }
    }
}
