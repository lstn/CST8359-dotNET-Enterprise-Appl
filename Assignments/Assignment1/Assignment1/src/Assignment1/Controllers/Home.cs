using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Assignment1.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment1.Controllers
{
    public class Home : Controller
    {
        private Assignment2DataContext _blogContext;

        public Home(Assignment2DataContext context)
        {
            _blogContext = context;
        }


        // GET: /<controller>/
        [HttpGet, HttpPost]
        public IActionResult Register()
        {
            if (HttpContext.Request.Method.ToString() == "POST")
            {
                try
                {
                    var newUser = new User
                    {
                        RoleId = Int32.Parse(HttpContext.Request.Form["RoleId"]),
                        FirstName = HttpContext.Request.Form["FirstName"],
                        LastName = HttpContext.Request.Form["LastName"],
                        EmailAddress = HttpContext.Request.Form["EmailAddress"],
                        Password = HttpContext.Request.Form["Password"]
                    };
                    _blogContext.Users.Add(newUser);
                    _blogContext.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    return View();
                }

            }

            return View();
        }

        // GET: /<controller>/
        [HttpGet, HttpPost]
        public IActionResult Login()
        {
            if (HttpContext.Request.Method.ToString() == "POST")
            {
                if (_blogContext.Users.Any(user => user.EmailAddress.Equals(HttpContext.Request.Form["EmailAddress"])))
                {
                    var curUser = (from c in _blogContext.Users where c.EmailAddress == HttpContext.Request.Form["EmailAddress"] select c).FirstOrDefault();

                    if (curUser.Password == HttpContext.Request.Form["Password"])
                    {
                        HttpContext.Session.SetInt32("isLoggedIn", 1);
                        HttpContext.Session.SetInt32("userId", curUser.UserId);
                        HttpContext.Session.SetString("userFirstName", curUser.FirstName);
                        HttpContext.Session.SetString("userLastName", curUser.LastName);
                        HttpContext.Session.SetInt32("userRoleId", curUser.RoleId);

                        return RedirectToAction("Index");
                    }
                } else
                {
                    return View();
                }
            }
            return View();
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View(_blogContext.BlogPosts.ToList());
        }

        // GET: /<controller>/
        [HttpGet, HttpPost]
        public IActionResult DisplayFullBlogPost(int postId)
        {
            string rpath = HttpContext.Request.Path.Value.Split('/').Last();
            int pId = Int32.Parse(rpath);
            if (HttpContext.Request.Method.ToString() == "POST" && HttpContext.Session.GetInt32("isLoggedIn").Equals(1))
            {
                try
                {
                    var newComment = new Comment
                    {
                        BlogPostId = pId,
                        Content = HttpContext.Request.Form["CommentContent"],
                        UserId = (int) HttpContext.Session.GetInt32("userId")
                    };
                    _blogContext.Comments.Add(newComment);
                    _blogContext.SaveChanges();
                }
                catch (Exception)
                {
                    
                }
            }
            var bpost = _blogContext.BlogPosts.Where(bp => bp.BlogPostId.Equals(pId)).FirstOrDefault();
            var author = _blogContext.Users.Where(user => user.UserId.Equals(bpost.UserId)).FirstOrDefault();
            var comments = _blogContext.Comments.Where(comm => comm.BlogPostId.Equals(pId)).ToList();
            var commenters = _blogContext.Users.ToList();
            var tupleView = new Tuple<BlogPost, User, IEnumerable<Comment>, IEnumerable<User>>(bpost, author, comments, commenters);
            return View(tupleView);
        }

        // GET: /<controller>/
        [HttpGet, HttpPost]
        public IActionResult AddBlogPost()
        {
            if (HttpContext.Request.Method.ToString() == "POST" && HttpContext.Session.GetInt32("isLoggedIn").Equals(1))
            {
                try
                {
                    var newBlogPost = new BlogPost
                    {
                        UserId = (int) HttpContext.Session.GetInt32("userId"),
                        Title = HttpContext.Request.Form["BlogTitle"],
                        Content = HttpContext.Request.Form["BlogContent"],
                        Posted = DateTime.Now
                    };
                    
                    _blogContext.BlogPosts.Add(newBlogPost);
                    _blogContext.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    return View();
                }
            } else if (!HttpContext.Session.GetInt32("isLoggedIn").Equals(1))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // GET: /<controller>/
        [HttpGet, HttpPost]
        public IActionResult EditBlogPost(int postId)
        {
            string rpath = HttpContext.Request.Path.Value.Split('/').Last();
            int pId = Int32.Parse(rpath);
            var bpost = _blogContext.BlogPosts.Where(bp => bp.BlogPostId.Equals(pId)).FirstOrDefault();

            if (HttpContext.Request.Method.ToString() == "POST" 
                    && HttpContext.Session.GetInt32("isLoggedIn").Equals(1) 
                    && HttpContext.Session.GetInt32("userRoleId").Equals(2))
            {
                try
                {
                    bpost.Title = HttpContext.Request.Form["BlogTitle"];
                    bpost.Content = HttpContext.Request.Form["BlogContent"];
                    bpost.Posted = DateTime.Now;

                    _blogContext.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    return View(bpost);
                }
            } 
            else if (!HttpContext.Session.GetInt32("isLoggedIn").Equals(1))
            {
                return RedirectToAction("Login");
            }
            else if (!HttpContext.Session.GetInt32("userRoleId").Equals(2))
            {
                return RedirectToAction("Index");
            }

                return View(bpost);
        }


    }
}
