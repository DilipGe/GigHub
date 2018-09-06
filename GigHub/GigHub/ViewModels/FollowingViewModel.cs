using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> User { get; set; }
    }
}