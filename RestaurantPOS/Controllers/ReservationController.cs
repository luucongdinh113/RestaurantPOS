using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using RestaurantManagement.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantManagement.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/Reservation")]
        public IActionResult BookTable()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Home"); ;
            }
            return View(new ListBookInfoViewModel());
        }

        [HttpPost("/Reservation")]
        public async Task<IActionResult> BookTable(BookTableInfoViewModel bookTableInfo)
        {
            if (bookTableInfo.Type=="search")
            {
                bookTableInfo.From = new DateTime(bookTableInfo.OrderDate.Year, bookTableInfo.OrderDate.Month, bookTableInfo.OrderDate.Day, bookTableInfo.From.Hour, bookTableInfo.From.Minute, bookTableInfo.From.Second);
                bookTableInfo.To = new DateTime(bookTableInfo.OrderDate.Year, bookTableInfo.OrderDate.Month, bookTableInfo.OrderDate.Day, bookTableInfo.To.Hour, bookTableInfo.To.Minute, bookTableInfo.To.Second);
                bookTableInfo.From = TimeZoneInfo.ConvertTimeToUtc(bookTableInfo.From);
                bookTableInfo.To = TimeZoneInfo.ConvertTimeToUtc(bookTableInfo.To);
                ListBookInfoViewModel table = new ListBookInfoViewModel
                {
                    listtable = await _reservationService.GetAllTableEmptyAsync(bookTableInfo.From, bookTableInfo.To, bookTableInfo.People)
                };
                return View(table);
            }    
           if(bookTableInfo.Type=="book")
            {
                return RedirectToAction("TableOrderedHistory", "Home");
            }
            return View();
        }
    }
}
