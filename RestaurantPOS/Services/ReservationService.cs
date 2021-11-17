
using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RestaurantPOS.Data;
using RestaurantPOS.Data.Entities;

namespace RestaurantManagement.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RestaurantDbContext _context;
        private readonly UserManager<Customer> _userManager;
        public ReservationService(UserManager<Customer> userManager, RestaurantDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<List<BookTableInfoViewModel>> GetAllTableEmptyAsync(DateTime From, DateTime To, int People)
        {
            var a = await (from T in _context.OderTable
                           join t in _context.Table on T.TableId equals t.Id
                     where ((From <= T.To && To >= T.From) || (From <= T.To && From >= T.From) || (To <= T.To && To >= T.From))&& t.PeopleCount>=People
                     select t).ToListAsync();
            if (a.Count==0)
            {
                var resultTable1 = await (from t in _context.Table
                                          where t.PeopleCount >= People
                                          orderby t.PeopleCount
                                          select new BookTableInfoViewModel
                                         {
                                             From = From,
                                             To = To,
                                             Name = t.Name,
                                             TableId = t.Id,
                                             People = t.PeopleCount
                                          }).ToListAsync();
                return resultTable1;
            }

            var b = await (from t in _context.Table
                                     where t.PeopleCount>=People
                                     select t).ToListAsync();
            var c = b.Except(a);
            var resultTable = new List<BookTableInfoViewModel>();
            foreach(var table in c)
            {
                resultTable.Add(new BookTableInfoViewModel
                {
                    From = From,
                    To = To,
                    Name = table.Name,
                    TableId = table.Id,
                    People = table.PeopleCount
                });
            }    
                return resultTable;
        }
        public async Task<bool> BookTableAsync(ClaimsPrincipal user, BookTableInfoViewModel table)
        {
            var customer = await _userManager.GetUserAsync(user);
            var resultTable = new OrderTable()
            {
                TableId = table.TableId,
                From = table.From,
                To=table.To,
                CustomerId= customer.Id
            };
            _context.OderTable.Add(resultTable);
            _context.SaveChanges();
            return true;
        }
    }
}
