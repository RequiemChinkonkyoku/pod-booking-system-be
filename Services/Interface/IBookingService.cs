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
        Task<CancelBookingResponse> CancelBooking(int id, int userId);
        Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request, int userId);
        Task<List<Booking>> GetUserBookings(int id);
        Task<Booking> GetBookingById(int id);
        Task<CreateBookingResponse> UpdateBookingStatus(int id);
        Task<UpdateBookingResponse> UpdateBooking(int bookingId, UpdateBookingRequest request, int userId);
    }
}
