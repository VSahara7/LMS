using Library_Management_System.Data;
using Library_Management_System.Models;
using Library_Management_System.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library_Management_System.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;

        }

      
     
        public IActionResult Index()
        {
            IEnumerable<Book> objBook = _db.Books;
            foreach(var obj in objBook)
            {
                obj.BookPublishers = _db.BookPublishers.FirstOrDefault(u => u.PublisherId == obj.PublisherId);
            }
            return View(objBook);

        }
        public IActionResult IndexPub()
        {
            IEnumerable<BookPublisher> objBook = _db.BookPublishers;
            return View(objBook);

        }
        //GET

        public IActionResult Managcategory()
        {
            IEnumerable<Book> objBook = _db.Books;
            foreach (var obj in objBook)
            {
                obj.BookPublishers = _db.BookPublishers.FirstOrDefault(u => u.PublisherId == obj.PublisherId);
            }
            return View(objBook);
        }
        
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM()
            {
                Books = new Book(),
                TypeDropDown = _db.BookPublishers.Select(i => new SelectListItem
                {
                    Text = i.PublName,
                    Value = i.PublisherId.ToString()
                })
            };
            return View(bookVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookVM obj)
        {
            if (obj.Books.BookName == obj.Books.Author.ToString())
            {
                ModelState.AddModelError("name", "The Book author cannot exactly match the Book name.");
            }
            if (ModelState.IsValid)
            {
                _db.Books.Add(obj.Books);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult CreatePub()
        {
   
         
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePub(BookPublisher obj)
        {
         
            if (ModelState.IsValid)
            {
                _db.BookPublishers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("IndexPub");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.Books.Find(id);
          
            if (BookFromDb == null)
            {
                return NotFound();
            }
            ViewBag.TypeDropDown = _db.BookPublishers.Select(i => new SelectListItem
            {
                Text = i.PublName,
                Value = i.PublisherId.ToString()
            });

            return View(BookFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book obj)
        {
            if (ModelState.IsValid)
            {
                
               
                _db.Books.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult EditPub(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.BookPublishers.Find(id);

            return View(BookFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPub(BookPublisher obj)
        {
            if (ModelState.IsValid)
            {


                _db.BookPublishers.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("IndexPub");
            }
            return View(obj);
        }
        public IActionResult Borrow(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.Books.Find(id);
            if (BookFromDb == null)
            {
                return NotFound();
            }
            return View(BookFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrow(Book obj)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.Books.Find(id);
            ViewBag.TypeDropDown = _db.BookPublishers.Select(i => new SelectListItem
            {
                Text = i.PublName,
                Value = i.PublisherId.ToString()
            });

            if (BookFromDb == null)
            {
                return NotFound();
            }
            return View(BookFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Book obj)
        {
           /* var obj = _db.Books.Find(obj);
            if (obj == null)
            {
                return NotFound();
            }*/
            _db.Books.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeletePub(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.BookPublishers.Find(id);

            if (BookFromDb == null)
            {
                return NotFound();
            }
            return View(BookFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePubPOST(BookPublisher obj)
        {
            /* var obj = _db.Books.Find(obj);
             if (obj == null)
             {
                 return NotFound();
             }*/
            _db.BookPublishers.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("IndexPub");
        }
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //POST-Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(User obj, string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = "/";
            }
            var row = _db.Users.Where(model => model.UserMail == obj.UserMail && model.UserPassword == obj.UserPassword).FirstOrDefault();
            if (row != null)
            {
                string userName = obj.UserMail;
                var claims = new List<Claim>();
                claims.Add(new Claim("username", userName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);



            }
            TempData["Error"] = "Error. Username or Password is invalid";
            return View("Login");
        }
      /*  [Authorize]*/
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");

        }
        [HttpGet("denied")]
        public ActionResult Denied()
        {
            return View();
        }



    }
}
