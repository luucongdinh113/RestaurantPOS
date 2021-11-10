
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
                     where (From <= T.To && To >= T.From) || (From <= T.To && From >= T.From) || (To <= T.To && To >= T.From)
                     select new
                     {
                         TableID = T.TableId
                     }).ToListAsync();
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
            var resultTable = await (from t in _context.Table
                                     from t1 in (from T in _context.OderTable
                                                where (From <= T.To && To >= T.From) || (From <= T.To && From >= T.From) || (To <= T.To && To >= T.From)
                                                select new{TableID = T.TableId})
                                     where t.Id != t1.TableID && t.PeopleCount>=People
                                     orderby t.PeopleCount
                                     select new BookTableInfoViewModel
                                     {
                                       From = From,
                                       To = To,
                                       Name = t.Name,
                                       TableId = t.Id,
                                       People=t.PeopleCount
                                      }).ToListAsync();
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
