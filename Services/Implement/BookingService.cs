using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class BookingService : IBookingService
    {
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IRepositoryBase<BookingStatus> _bookingStatusRepo;
        private readonly IRepositoryBase<BookingDetail> _bookingDetailRepo;
        private readonly IRepositoryBase<Pod> _podRepo;
        private readonly IRepositoryBase<PodType> _podTypeRepo;
        private readonly IRepositoryBase<Schedule> _scheduleRepo;
        private readonly IRepositoryBase<Slot> _slotRepo;
        private readonly IRepositoryBase<User> _userRepo;

        public BookingService(IRepositoryBase<Booking> bookingRepo,
                              IRepositoryBase<BookingStatus> bookingStatusRepo,
                              IRepositoryBase<BookingDetail> bookingDetailRepo,
                              IRepositoryBase<Pod> podRepo,
                              IRepositoryBase<PodType> podTypeRepo,
                              IRepositoryBase<Schedule> scheduleRepo,
                              IRepositoryBase<Slot> slotRepo,
                              IRepositoryBase<User> userRepo)
        {
            _bookingRepo = bookingRepo;
            _bookingStatusRepo = bookingStatusRepo;
            _bookingDetailRepo = bookingDetailRepo;
            _podRepo = podRepo;
            _podTypeRepo = podTypeRepo;
            _scheduleRepo = scheduleRepo;
            _slotRepo = slotRepo;
            _userRepo = userRepo;
        }

        public async Task<List<Booking>> GetUserBookings(int id)
        {
            var bookingList = await _bookingRepo.GetAllAsync();

            var userBookings = bookingList.Where(b => b.UserId == id).ToList();

            return userBookings;
        }

        public async Task<CancelBookingResponse> CancelBooking(int id, int userId)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return new CancelBookingResponse { Success = false, Message = "The BookingId does not exist." };
            }

            if (booking.UserId != userId)
            {
                return new CancelBookingResponse { Success = false, Message = "The Booking does not belong to this user." };
            }

            booking.BookingStatusId = 1;

            try
            {
                await _bookingRepo.UpdateAsync(booking);
            }
            catch (Exception ex)
            {
                return new CancelBookingResponse { Success = false, Message = "Unable to cancel the Booking with Id: " + id };
            }

            booking.BookingStatus = await _bookingStatusRepo.FindByIdAsync(1);

            return new CancelBookingResponse { Success = true, Booking = booking };
        }

        public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request)
        {
            if (request.UserId <= 0 ||
                request.ArrivalDate <= default(DateOnly) ||
                request.PodId <= 0 ||
                request.ScheduleIds == null ||
                !request.ScheduleIds.Any())
            {
                return new CreateBookingResponse { Success = false, Message = "All fields must be provided" };
            }

            var pod = await _podRepo.FindByIdAsync(request.PodId);

            if (pod == null)
            {
                return new CreateBookingResponse { Success = false, Message = "Invalid PodId: " + request.PodId };
            }

            foreach (var scheduleId in request.ScheduleIds)
            {
                var schedule = await _scheduleRepo.FindByIdAsync(scheduleId);

                if (schedule == null)
                {
                    return new CreateBookingResponse { Success = false, Message = "Invalid ScheduleId: " + scheduleId };
                }

                var slots = await _slotRepo.GetAllAsync();

                if (slots.Any())
                {
                    var bookedSlot = slots.FirstOrDefault(s => s.Date == request.ArrivalDate &&
                                                               s.PodId == request.PodId &&
                                                               s.ScheduleId == scheduleId &&
                                                               s.Status == 0);

                    if (bookedSlot != null)
                    {
                        bookedSlot.Schedule = await _scheduleRepo.FindByIdAsync(bookedSlot.ScheduleId.Value);

                        return new CreateBookingResponse
                        {
                            Success = false,
                            Message = "There is an existing booking between " + bookedSlot.Schedule.StartTime + " and " + bookedSlot.Schedule.EndTime
                        };
                    }
                }
            }

            var podPrice = _podTypeRepo.FindByIdAsync(pod.PodTypeId).Result.Price;

            var booking = new Booking
            {
                BookingPrice = podPrice,
                CreatedTime = DateTime.UtcNow,
                BookingStatusId = 2,
                UserId = request.UserId
            };

            try
            {
                await _bookingRepo.AddAsync(booking);
            }
            catch (Exception ex)
            {
                return new CreateBookingResponse { Success = false, Message = "There has been an error creating Booking." };
            }

            foreach (var scheduleId in request.ScheduleIds)
            {
                var slot = new Slot
                {
                    Status = 1,
                    Date = request.ArrivalDate,
                    ScheduleId = scheduleId,
                    PodId = request.PodId
                };

                try
                {
                    await _slotRepo.AddAsync(slot);
                }
                catch (Exception ex)
                {
                    return new CreateBookingResponse { Success = false, Message = "There has been an error creating Slot." };
                }

                var bookingDetail = new BookingDetail
                {
                    ArrivalDate = request.ArrivalDate,
                    BookingId = booking.Id,
                    SlotId = slot.Id
                };

                try
                {
                    await _bookingDetailRepo.AddAsync(bookingDetail);

                    slot.BookingDetailId = bookingDetail.Id;
                    await _slotRepo.UpdateAsync(slot);
                }
                catch (Exception ex)
                {
                    return new CreateBookingResponse { Success = false, Message = "There has been an error creating BookingDetail." };
                }
            }

            return new CreateBookingResponse { Success = true, Booking = booking };
        }

        public async Task<Booking> GetBookingById(int id)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return null;
            }

            var user = await _userRepo.FindByIdAsync(booking.UserId);

            booking.User = user;

            return booking;
        }

        public async Task<CreateBookingResponse> UpdateBookingStatus(int id)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return new CreateBookingResponse { Success = false, Message = "There is no booking with the id " + id };
            }

            booking.BookingStatusId = 4;

            try
            {
                await _bookingRepo.UpdateAsync(booking);
            }
            catch (Exception ex)
            {
                return new CreateBookingResponse { Success = false, Message = "There has been an error updating the appointment. " };
            }

            return new CreateBookingResponse { Success = true, Booking = booking };
        }


    }
}
