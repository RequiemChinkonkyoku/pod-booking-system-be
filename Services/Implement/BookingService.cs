using Models;
using Models.DTOs;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public async Task<GetBookingResponse> GetAllBookings()
        {
            var bookingList = await _bookingRepo.GetAllAsync();

            return new GetBookingResponse { Bookings = bookingList };
        }

        public async Task<GetBookingResponse> GetUserBookings(int id)
        {
            var bookingList = await _bookingRepo.GetAllAsync();

            var userBookings = bookingList.Where(b => b.UserId == id).ToList();

            return new GetBookingResponse { Bookings = userBookings };
        }

        public async Task<GetBookingResponse> GetBookingById(int id, int userId)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return new GetBookingResponse { Success = false, Message = "Unable to find booking with id " + id };
            }

            if (booking.UserId != userId)
            {
                return new GetBookingResponse { Success = false, Message = "The booking does not belong to this user" };
            }

            var user = await _userRepo.FindByIdAsync(userId);

            booking.User = user;

            return new GetBookingResponse { Success = true, Booking = booking };
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

        public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request, int userId)
        {
            if (userId <= 0 ||
                request.ArrivalDate <= DateOnly.MinValue ||
                request.PodId <= 0 ||
                request.ScheduleIds?.Any() != true)
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
                                                               s.Status == 1);

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
                UserId = userId
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
                return new CreateBookingResponse { Success = false, Message = "There has been an error updating the appointment." };
            }

            return new CreateBookingResponse { Success = true, Booking = booking };
        }

        public async Task<UpdateBookingResponse> UpdateBooking(int bookingId, UpdateBookingRequest request, int userId)
        {
            if (userId <= 0 || bookingId <= 0)
            {
                return new UpdateBookingResponse { Success = false, Message = "UserId and BookingId must be provided." };
            }

            if (request.NewPodId <= 0 &&
                request.NewArrivalDate <= DateOnly.MinValue &&
                request.NewScheduleIds?.Any() != true)
            {
                return new UpdateBookingResponse { Success = false, Message = "Update data cannot be empty." };
            }

            var booking = await _bookingRepo.FindByIdAsync(bookingId);

            if (booking == null)
            {
                return new UpdateBookingResponse { Success = false, Message = "There is no booking with the id " + bookingId + "." };
            }

            if (booking.UserId != userId)
            {
                return new UpdateBookingResponse { Success = false, Message = "The booking does not belong to this user." };
            }

            if (booking.BookingStatusId != 2 && booking.BookingStatusId != 3)
            {
                return new UpdateBookingResponse { Success = false, Message = "Only Pending or Reserved bookings are able to be updated." };
            }

            var pod = await _podRepo.FindByIdAsync(request.NewPodId);

            if (pod == null)
            {
                return new UpdateBookingResponse { Success = false, Message = "There is no pod with the id " + request.NewPodId + "." };
            }

            foreach (var newScheduleId in request.NewScheduleIds)
            {
                var newSchedule = await _scheduleRepo.FindByIdAsync(newScheduleId);

                if (newSchedule == null)
                {
                    return new UpdateBookingResponse { Success = false, Message = "Invalid ScheduleId: " + newScheduleId };
                }

                var slots = await _slotRepo.GetAllAsync();

                if (slots.Any())
                {
                    var newBookedSlot = slots.FirstOrDefault(s => s.Date == request.NewArrivalDate &&
                                                               s.PodId == request.NewPodId &&
                                                               s.ScheduleId == newScheduleId &&
                                                               s.Status == 1);

                    if (newBookedSlot != null)
                    {
                        newBookedSlot.Schedule = await _scheduleRepo.FindByIdAsync(newBookedSlot.ScheduleId.Value);

                        return new UpdateBookingResponse
                        {
                            Success = false,
                            Message = "There is an existing booking between " + newBookedSlot.Schedule.StartTime + " and " + newBookedSlot.Schedule.EndTime
                        };
                    }
                }
            }
            var bookingDetails = await _bookingDetailRepo.GetAllAsync();

            var oldDetails = bookingDetails.FindAll(bd => bd.BookingId == bookingId);

            var oldSlots = new List<Slot>();

            foreach (var detail in oldDetails)
            {
                var slot = await _slotRepo.FindByIdAsync(detail.SlotId);

                oldSlots.Add(slot);
            }

            try
            {
                foreach (var slot in oldSlots)
                {
                    await _slotRepo.DeleteAsync(slot);
                }
            }
            catch (Exception ex)
            {
                return new UpdateBookingResponse { Success = false, Message = "There has been an error removing old slots." };
            }

            try
            {
                foreach (var detail in oldDetails)
                {
                    await _bookingDetailRepo.DeleteAsync(detail);
                }
            }
            catch (Exception ex)
            {
                return new UpdateBookingResponse { Success = false, Message = "There has been an error removing old details." };
            }

            foreach (var newScheduleId in request.NewScheduleIds)
            {
                var slot = new Slot
                {
                    Status = 1,
                    Date = request.NewArrivalDate,
                    ScheduleId = newScheduleId,
                    PodId = request.NewPodId
                };

                try
                {
                    await _slotRepo.AddAsync(slot);
                }
                catch (Exception ex)
                {
                    return new UpdateBookingResponse { Success = false, Message = "There has been an error updating Slot." };
                }

                var bookingDetail = new BookingDetail
                {
                    ArrivalDate = request.NewArrivalDate,
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
                    return new UpdateBookingResponse { Success = false, Message = "There has been an error updating BookingDetail." };
                }
            }

            return new UpdateBookingResponse { Success = true, Booking = booking };
        }

        public async Task<FinishBookingResponse> FinishBooking(int id)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return new FinishBookingResponse { Success = false, Message = "There are no booking with id " + id };
            }

            if (booking.BookingStatusId != 4)
            {
                return new FinishBookingResponse { Success = false, Message = "Only On-going bookings can be finished." };
            }

            booking.BookingStatusId = 5;

            try
            {
                await _bookingRepo.UpdateAsync(booking);
            }
            catch (Exception ex)
            {
                return new FinishBookingResponse { Success = false, Message = "There has been an error updating the booking." };
            }

            return new FinishBookingResponse { Success = true, Booking = booking };
        }
    }
}
