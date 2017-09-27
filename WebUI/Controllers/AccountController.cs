using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using Domain.Abstract;
using WebUI.Infrastructure;
using WebUI.Models;
using WebUI.Providers;

namespace WebUI.Controllers
{
    using System;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    namespace CustomAuth.Controllers
    {
        [Authorize]
        public class AccountController : Controller
        {
            private readonly IUserRepository _repository;

            public AccountController(IUserRepository repository)
            {
                this._repository = repository;
            }


            [AllowAnonymous]
            public ActionResult Login(string returnUrl)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home"); //TODO:RIGHT WAY
                }

                ViewBag.ReturnUrl = returnUrl;

                return View();
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    if (new CustomMembershipProvider().ValidateUser(viewModel.UserName, viewModel.Password))
                    {
                        if (new CustomRoleProvider().IsUserInRole(viewModel.UserName, "banned"))
                        {
                            return View("_BannedUser");
                        }

                        FormsAuthentication.SetAuthCookie(viewModel.UserName, false);

                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return RedirectToAction("Index", "Home");//TODO:right way
                    }
                }

                ModelState.AddModelError("", "Incorrect login or password.");

                return View(viewModel);
            }


            public ActionResult LogOff()
            {
                FormsAuthentication.SignOut();

                return RedirectToAction("Login", "Account");
            }


            [HttpGet]
            [AllowAnonymous]
            public ActionResult Register()
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");//TODO:right way
                }

                return View();
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public ActionResult Register(RegisterViewModel viewModel)
            {
                if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
                {
                    ModelState.AddModelError("Captcha", "Incorrect input.");

                    return View(viewModel);
                }

                var anyUser = _repository.Users.Any(u => u.Email.Contains(viewModel.Email));

                if (anyUser)
                {
                    ModelState.AddModelError("", "User with this address already registered.");

                    return View(viewModel);
                }

                if (ModelState.IsValid)
                {
                    var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                        .CreateUser(viewModel.Email, viewModel.Password, viewModel.Email);

                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.Email, false);

                        return RedirectToAction("Index", "Home");//TODO:right way
                    }

                    ModelState.AddModelError("", "Error registration.");
                }

                return View(viewModel);
            }

            //В сессии создаем случайное число от 1111 до 9999.
            //Создаем в ci объект CatchaImage
            //Очищаем поток вывода
            //Задаем header для mime-типа этого http-ответа будет "image/jpeg" т.е. картинка формата jpeg.
            //Сохраняем bitmap в выходной поток с форматом ImageFormat.Jpeg
            //Освобождаем ресурсы Bitmap
            //Возвращаем null, так как основная информация уже передана в поток вывод
            [AllowAnonymous]
            public ActionResult Captcha()
            {
                Session[CaptchaImage.CaptchaValueKey] =
                    new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
                var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

                // Change the response headers to output a JPEG image.
                this.Response.Clear();
                this.Response.ContentType = "image/jpeg";

                // Write the image to the response stream in JPEG format.
                ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

                // Dispose of the CAPTCHA image object.
                ci.Dispose();

                return null;
            }
        }
    }
}