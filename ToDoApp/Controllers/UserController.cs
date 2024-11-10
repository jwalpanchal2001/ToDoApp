using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);

                if (user != null && PasswordHelper.VerifyPassword(request.Password, user.Password))
                {
                    // If password matches, set authentication cookie
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(request);
        }


        // GET: User/ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: User/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);
                if (user != null)
                {
                    // Here you should implement email sending with a reset link
                    TempData["Message"] = "Password reset link has been sent to your email.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "No user found with that email address.");
                }
            }

            return View(request);
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); // Clears the authentication cookie
            return RedirectToAction("Login");
        }

    }
}