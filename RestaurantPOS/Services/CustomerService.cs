using Microsoft.AspNetCore.Identity;
using RestaurantPOS.Data;
using RestaurantPOS.Data.Entities;
using RestaurantPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace RestaurantPOS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly RestaurantDbContext _context;
        public CustomerService(
            SignInManager<Customer> signInManager,
            UserManager<Customer> userManager,
            RestaurantDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            var customer = await _userManager.FindByNameAsync(username);
            if (customer == null)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (registerViewModel.Password != registerViewModel.RePassword) return false;

            var customer = await _userManager.FindByNameAsync(registerViewModel.UserName);
            if (customer != null)
            {
                return false;
            }

            var newCustomer = new Customer()
            {
                Id = System.Guid.NewGuid(),
                UserName = registerViewModel.UserName,
                PhoneNumber = registerViewModel.PhoneNumber,
                FullName = registerViewModel.FullName,
                Gender = registerViewModel.Gender,
                Birthday = registerViewModel.Birthday,
                VIP = false,
            };

            var result = await _userManager.CreateAsync(newCustomer, registerViewModel.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<List<TableHistoryViewModel>> GetTableHistoryAsync(ClaimsPrincipal user)
        {
            var customer = await _userManager.GetUserAsync(user);
            if (customer == null)
                return new List<TableHistoryViewModel>();
            var tableOrderHistory = await (from f in _context.OderTable
                                           join g in _context.Table on f.TableId equals g.Id
                                           where f.CustomerId == customer.Id
                                           select new TableHistoryViewModel
                                           {
                                               Id = f.Id,
                                               From = f.From,
                                               To = f.To,
                                               TableName = g.Name,
                                               PeopleCount = g.PeopleCount,
                                           }).ToListAsync();
            return tableOrderHistory;
        }
    }
}
