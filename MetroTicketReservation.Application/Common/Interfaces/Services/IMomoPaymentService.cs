using MetroTicketReservation.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Application.Common.Interfaces.Services
{
    public interface IMomoPaymentService
    {
        Task<MomoCreatePaymentResult> CreatePaymentAsync(MomoPaymentRequest request);
        MomoPaymentExecutionResult ProcessPaymentCallback(IQueryCollection collection);
    }
}
