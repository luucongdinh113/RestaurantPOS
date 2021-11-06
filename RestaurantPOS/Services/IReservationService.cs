using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantManagement.Services
{
    public interface IReservationService
    {
        Task<List<BookTableInfoViewModel>> GetAllTableEmptyAsync(DateTime from, DateTime to,int People);
        Task<bool> BookTableAsync(ClaimsPrincipal user, BookTableInfoViewModel table);
    }
}
