using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantPOS.Models;
using RestaurantPOS.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        [HttpGet("/ResetPassword")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost("/ResetPassword")]
        public async Task<IActionResult> ResetPassword(RegisterViewModel resetPassword)
        {
            var check = await _customerService.CheckPasswordAsync(resetPassword);
            if (!check)
            {
                ModelState.Clear();
                return View();
            }

            await _customerService.ResetPasswordAsync(resetPassword);
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> ShowToCart()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Home");
            var cart = await _customerService.ShowToCartAsync(User);
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> ShowToCart(CartDetailViewModel cartdetailvm)
        {
            if (cartdetailvm.Type == "-")
            {
                cartdetailvm.Quantity--;
            }
            if (cartdetailvm.Type == "+")
            {
                cartdetailvm.Quantity++;
            }
            var cart = await _customerService.ShowToCartAsync(User, cartdetailvm);
            return View(cart);
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
            return RedirectToAction("PaymentHistory", "Home");
        }

        public async Task<IActionResult> PaymentHistory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Home");
            }
            var pmHistory = await _customerService.GetPaymentHistoryAsync(User);
            return View(pmHistory);
        }
        public async Task<IActionResult> PaymentDetailHistory(Guid id)
        {
            var paymentDetail = await _customerService.GetPaymentDetailAsync(id);
            return View(paymentDetail);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
