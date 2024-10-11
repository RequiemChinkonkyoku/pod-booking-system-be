using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IBookingService
    {
        Task<GetBookingResponse> GetAllBookings();
        Task<GetBookingResponse> GetUserBookings(int id);
        Task<GetBookingResponse> GetBookingById(int id, int userId);
        Task<CancelBookingResponse> CancelBooking(int id, int userId);
        Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request, int userId);
        Task<CreateBookingResponse> UpdateBookingStatus(int id);
        Task<UpdateBookingResponse> UpdateBooking(int bookingId, UpdateBookingRequest request, int userId);
    }
}
