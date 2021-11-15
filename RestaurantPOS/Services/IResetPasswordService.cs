using RestaurantPOS.Models;
using System;
using System.Threading.Tasks;

namespace RestaurantPOS.Services
{
    public interface IResetPasswordService
    {
        public Task<bool> SendMailResetPasswordAsync(string username);
        public Task<bool> ResetPasswordAsync(Guid customerId, ResetPasswordViewModel resetPasswordViewModel);
    }
}
