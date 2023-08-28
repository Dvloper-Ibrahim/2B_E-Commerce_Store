using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface IPaymentServices
    {
        Task<List<PaymentDTO>> GetAllPayments();

        Task<PaymentDTO> GetPaymentById(int paymentId);
        Task<PaymentDTO> AddPayment(PaymentDTO paymentDTO);
        Task<PaymentDTO> UpdatePayment(int paymentId, PaymentDTO paymentDTO);
        Task DeletePayment(int paymentId);
    }
}
