namespace ZekrHakeem_v11.Services
{
    public class HomeService
    {
        public HomeViewModel GetHomeData()
        {
            return new HomeViewModel
            {
                //Slides = _repo.GetSlides(),
                //Counts = _repo.GetCounts(),
                //Management = _repo.GetMembers()
            };
        }

    }
}
