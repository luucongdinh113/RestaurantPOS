using RestaurantPOS.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantPOS.Services
{
    public interface ICustomerService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(RegisterViewModel registerViewModel);
        Task SignOutAsync();
        Task<bool> CheckPasswordAsync(RegisterViewModel resetPassword);
        Task<bool> ResetPasswordAsync(RegisterViewModel resetPassword);
        Task<List<TableHistoryViewModel>> GetTableHistoryAsync(ClaimsPrincipal user);
        Task<PaymentViewModel> GetBillToPayAsync(ClaimsPrincipal user);
        Task<CartViewModel> ShowToCartAsync(ClaimsPrincipal user);
        Task<CartViewModel> ShowToCartAsync(ClaimsPrincipal user, CartDetailViewModel cartdetailvm);
        Task UpdatePaymentMethodAsync(ClaimsPrincipal user, PaymentViewModel billPaymentVM);
        Task<List<PaymentHistoryViewModel>> GetPaymentHistoryAsync(ClaimsPrincipal user);
        Task<List<PaymentDetailViewModel>> GetPaymentDetailAsync(Guid billId);
    }
}
