using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {            
            GigFormViewmodel viewModel = new GigFormViewmodel();
            viewModel.Genres = _context.Genres.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewmodel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }
               
            var userId = User.Identity.GetUserId();
            var artist = _context.Users.Single(u => u.Id == userId);
            var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);
            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                Venu = viewModel.Venu,
                GenreId = viewModel.Genre
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }     
        
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = _context.Attendances
                                .Where(a => a.AttendeeId == userId)
                                .Select(a => a.Gig)
                                .Include(g => g.Artist)
                                .Include(g => g.Genre)
                                .ToList();

            GigsViewModel viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs", viewModel);
        }  

        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var followingArtists = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            FollowingViewModel viewModel = new FollowingViewModel()
            {
                User = followingArtists
            };

            return View(viewModel);
        }
    }
}