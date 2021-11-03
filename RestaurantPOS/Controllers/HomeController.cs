using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantPOS.Models;
using RestaurantPOS.Services;
using System.Security.Claims;
using RestaurantManagement.Models;

namespace RestaurantPOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService; 
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var loginSucess = await _customerService.LoginAsync(loginViewModel.UserName, loginViewModel.Password);

            if (!loginSucess)
            {
                return View(loginViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var registerSucess = await _customerService.RegisterAsync(registerViewModel);

            if (!registerSucess)
            {
                return View(registerViewModel);
            }

            return RedirectToAction("Login");
        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _customerService.SignOutAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> TableOrderedHistory()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Home");
            var TbHistory = await _customerService.GetTableHistoryAsync(User);
            return View(TbHistory);
        }

        [HttpGet("/Payment")]
        public async Task<IActionResult> Payment()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Home");
            var payment = await _customerService.GetBillToPayAsync(User);
            return View(payment);
        }

        [HttpPost("/Payment")]
        public async Task<IActionResult> Payment(PaymentViewModel billPaymentVM)
        {
            await _customerService.UpdatePaymentMethodAsync(User, billPaymentVM);
            return RedirectToAction("MenuFood", "Menu");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
