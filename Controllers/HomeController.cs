using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewBelt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace NewBelt.Controllers
{
        public class HomeController : Controller
        {
            private BeltContext _context;

        public HomeController(BeltContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.message = HttpContext.Session.GetString("message");
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Register(ViewUsers user, string password)
        {

        var useremail = _context.newusers.SingleOrDefault(e => e.Email == user.Email);
                if(ModelState.IsValid && useremail == null){ 
                var input = password;
                PasswordHasher<ViewUsers> Hasher = new PasswordHasher<ViewUsers>();
                user.Password = Hasher.HashPassword(user, user.Password);

                HttpContext.Session.SetString( "UserName",user.Alias);
                HttpContext.Session.SetInt32( "UserID",user.usersId);
                
                return RedirectToAction("Create",user);
                }
            
                ViewBag.emailmessage = "Email exsits already.";
                ViewBag.Errors = ModelState.Values;
                return View("index");
    }
        public IActionResult Create(Users user)
        { 
                //Save your user object to the database
                _context.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32( "UserID",user.UsersId);
                return RedirectToAction("DashBoard");
        } 
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            
            var useremail = _context.newusers.SingleOrDefault(e => e.Email == Email);
            if(useremail != null && Password != null)
            {
                var Hasher = new PasswordHasher<Users>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(useremail, useremail.Password, Password))
                {
                HttpContext.Session.SetString( "UserName",useremail.Alias);
                HttpContext.Session.SetInt32( "UserID",useremail.UsersId);
                
                    return RedirectToAction("DashBoard");
                }
            }
            ViewBag.message = "Email and Password doesnt match.";
                return View("index");
            
        }
        [Route("DashBoard")]
        public IActionResult DashBoard()
        {
            int? userID =  HttpContext.Session.GetInt32( "UserID");
            if(userID == null)
            {
                return View("index");
            }
            ViewBag.name = HttpContext.Session.GetString( "UserName");
            var allPost = _context.post.Include(r => r.PostThatAreLiked).ThenInclude(u => u.Users).ThenInclude(p=> p.Maker).OrderByDescending(d=>d.Likes).ToList();
            ViewBag.allPost = allPost;
            ViewBag.UsersId= userID;
            return View();
        }
        [HttpPost]
        [Route("CreateIdea")]
        public IActionResult CreateIdea(Post post)
        {
            int? userID = HttpContext.Session.GetInt32( "UserID");
            if(userID == null)
            {
                return View("index");
            }
            if(ModelState.IsValid){
            Post newPost = new Post ();
            newPost.Posts = post.Posts;
            newPost.UsersId =(int) userID;
            newPost.AliasName = HttpContext.Session.GetString( "UserName");
            _context.Add(newPost);
            _context.SaveChanges(); 
            return RedirectToAction("DashBoard");
            }
            ViewBag.Errors = ModelState.Values;
            return RedirectToAction("DashBoard");
            
        }
        [Route("home/Like/{PostId}")]
        public IActionResult Like(int PostId)
        {
            int? userID = HttpContext.Session.GetInt32( "UserID");
            if(userID == null)
            {
                return View("index");
            }
            var check = _context.likers.SingleOrDefault(c=> c.PostId ==PostId && c.UsersId == userID);
            if(check !=null)
            {
                return RedirectToAction("DashBoard");
            }
            var like = _context.post.SingleOrDefault(l=> l.PostId ==PostId);
            if(like.UsersId == userID)
            {
                return RedirectToAction("DashBoard");
            }
            Likers newLiker = new Likers();
            newLiker.UsersId = (int) userID;
            newLiker.PostId = PostId;
            like.Likes++;
            _context.Add(newLiker);
            _context.SaveChanges();
            return RedirectToAction("DashBoard");

        }
        [Route("home/delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            int? userID = HttpContext.Session.GetInt32( "UserID");
            if(userID == null)
            {
                return View("index");
            }
            var check = _context.post.SingleOrDefault(c=>c.PostId == PostId);
            if(check.UsersId != userID)
            {
                return View("Index");
            }
            _context.Remove(check);
            _context.SaveChanges();
            return RedirectToAction("DashBoard");
        }
        [Route("home/DisplayPeople/{PostId}/{UsersId}")]
        public IActionResult DisplayPeople(int PostId, int UsersId)
        {
            int? userID = HttpContext.Session.GetInt32( "UserID");
            if(userID == null)
            {
                return View("index");
            }
            // var personIdea= _context.post.Include(u=>u.Users).SingleOrDefault(u=>u.UsersId==UsersId);
            var idea = _context.post.SingleOrDefault(p=>p.PostId==PostId);
            var allpeeps = _context.likers.Where(a=>a.PostId == PostId).Include(m=>m.Users).ToList();
            ViewBag.idea = idea;
            // ViewBag.person = personIdea;
            ViewBag.allpeeps = allpeeps;
            return View();
        }
        [Route("home/display")]
        [Route("home/DisplayUser/{UsersId}")]
        public IActionResult DisplayUser(int UsersId)
        {
            int? userID = HttpContext.Session.GetInt32( "UserID");
            if(userID == null)
            {
                return View("index");
            }
            Users user = _context.newusers.SingleOrDefault(u=>u.UsersId == UsersId);
            var allpost = _context.post.Where(u=> u.UsersId == UsersId).ToList();
            var alllikes = _context.likers.Where(u=>u.UsersId == UsersId).ToList();
            ViewBag.user = user;
            ViewBag.allPost = allpost.Count;
            ViewBag.allLikes = alllikes.Count;
            return View();
        }
        [Route("/LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}