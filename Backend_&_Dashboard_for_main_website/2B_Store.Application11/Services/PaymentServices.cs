using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentServices(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<List<PaymentDTO>>(payments);
        }

        public async Task<PaymentDTO> GetPaymentById(int paymentId)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            return _mapper.Map<PaymentDTO>(payment);
        }

        public async Task<PaymentDTO> AddPayment(PaymentDTO paymentDTO)
        {
            var payment = _mapper.Map<Payment>(paymentDTO);
            payment = await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();
            return _mapper.Map<PaymentDTO>(payment);
        }

        public async Task<PaymentDTO> UpdatePayment(int paymentId, PaymentDTO paymentDTO)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(paymentId);
            if (existingPayment == null)
                throw new ArgumentException("Payment not found");

            _mapper.Map(paymentDTO, existingPayment);
            existingPayment = await _paymentRepository.UpdateAsync(existingPayment);
            await _paymentRepository.SaveChangesAsync();
            return _mapper.Map<PaymentDTO>(existingPayment);
        }

        public async Task DeletePayment(int paymentId)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(paymentId);
            if (existingPayment == null)
                throw new ArgumentException("Payment not found");

            await _paymentRepository.DeleteAsync(existingPayment);
            await _paymentRepository.SaveChangesAsync();
        }
    }
}
