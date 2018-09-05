using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<GigHub.Models.Gig> UpcomingGigs { get; set; }

        public bool ShowActions { get; set; }
    }
}