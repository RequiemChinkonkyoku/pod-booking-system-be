using Models;
using Models.DTOs;
using Repositories.Implement;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly IRepositoryBase<Membership> _membershipRepo;
        private readonly IRepositoryBase<Area> _areaRepo;
        private readonly IRepositoryBase<SelectedProduct> _selectedProductRepo;
        private readonly IRepositoryBase<Product> _productRepo;
        private readonly IMembershipService _memberService;

        public BookingService(IRepositoryBase<Booking> bookingRepo,
                              IRepositoryBase<BookingStatus> bookingStatusRepo,
                              IRepositoryBase<BookingDetail> bookingDetailRepo,
                              IRepositoryBase<Pod> podRepo,
                              IRepositoryBase<PodType> podTypeRepo,
                              IRepositoryBase<Schedule> scheduleRepo,
                              IRepositoryBase<Slot> slotRepo,
                              IRepositoryBase<User> userRepo,
                              IRepositoryBase<Membership> membershipRepo,
                              IRepositoryBase<SelectedProduct> selectedProductRepo,
                              IRepositoryBase<Product> productRepo,
                              IRepositoryBase<Area> areaRepo,
                              IMembershipService memberService)
        {
            _bookingRepo = bookingRepo;
            _bookingStatusRepo = bookingStatusRepo;
            _bookingDetailRepo = bookingDetailRepo;
            _podRepo = podRepo;
            _podTypeRepo = podTypeRepo;
            _scheduleRepo = scheduleRepo;
            _slotRepo = slotRepo;
            _userRepo = userRepo;
            _membershipRepo = membershipRepo;
            _areaRepo = areaRepo;
            _selectedProductRepo = selectedProductRepo;
            _productRepo = productRepo;
            _memberService = memberService;
        }

        public async Task<GetBookingResponse> GetAllBookings()
        {
            var bookingList = await _bookingRepo.GetAllAsync();
            var bookingOverviews = new List<BookingOverviewDto>();

            var bookingDetails = await _bookingDetailRepo.GetAllAsync();

            foreach (var booking in bookingList)
            {
                var arrivalDate = DateOnly.MinValue;
                var startTime = TimeOnly.MaxValue;
                var endTime = TimeOnly.MinValue;
                var podId = 0;
                var podName = "";
                var podTypeId = 0;
                var podType = new PodType();

                var userDetails = bookingDetails.Where(d => d.BookingId == booking.Id).ToList();

                foreach (var detail in userDetails)
                {
                    var slot = await _slotRepo.FindByIdAsync(detail.SlotId);

                    if (slot == null)
                    {
                        return new GetBookingResponse { Message = "Slot is null." };
                    }

                    arrivalDate = slot.ArrivalDate;

                    var schedule = await _scheduleRepo.FindByIdAsync(slot.ScheduleId.Value);

                    if (schedule == null)
                    {
                        return new GetBookingResponse { Message = "Schedule is null." };
                    }

                    if (schedule.StartTime < startTime)
                    {
                        startTime = schedule.StartTime;
                    }

                    if (schedule.EndTime > endTime)
                    {
                        endTime = schedule.EndTime;
                    }

                    var pod = await _podRepo.FindByIdAsync(slot.PodId.Value);

                    if (pod == null)
                    {
                        return new GetBookingResponse { Message = "Pod is null." };
                    }

                    podId = pod.Id;
                    podName = pod.Name;
                    podTypeId = pod.PodTypeId;
                    podType = await _podTypeRepo.FindByIdAsync(podTypeId);

                    if (podType == null)
                    {
                        return new GetBookingResponse { Message = "PodType is null." };
                    }
                }

                var bookingOverview = new BookingOverviewDto
                {
                    BookingId = booking.Id,
                    ArrivalDate = arrivalDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    StatusId = booking.BookingStatusId,
                    PodId = podId,
                    PodName = podName,
                    PodTypeId = podTypeId,
                    PodTypeName = podType.Name,
                };

                bookingOverviews.Add(bookingOverview);
            }

            return new GetBookingResponse { BookingOverview = bookingOverviews };
        }

        public async Task<GetBookingResponse> GetUserBookings(int id)
        {
            var bookingList = await _bookingRepo.GetAllAsync();
            var userBookings = bookingList.Where(b => b.UserId == id).ToList();
            var bookingOverviews = new List<BookingOverviewDto>();

            var bookingDetails = await _bookingDetailRepo.GetAllAsync();

            foreach (var booking in userBookings)
            {
                var arrivalDate = DateOnly.MinValue;
                var startTime = TimeOnly.MaxValue;
                var endTime = TimeOnly.MinValue;
                var podId = 0;
                var podName = "";
                var podTypeId = 0;
                var podType = new PodType();

                var userDetails = bookingDetails.Where(d => d.BookingId == booking.Id).ToList();

                foreach (var detail in userDetails)
                {
                    var slot = await _slotRepo.FindByIdAsync(detail.SlotId);

                    if (slot == null)
                    {
                        return new GetBookingResponse { Message = "Slot is null." };
                    }

                    arrivalDate = slot.ArrivalDate;

                    var schedule = await _scheduleRepo.FindByIdAsync(slot.ScheduleId.Value);

                    if (schedule == null)
                    {
                        return new GetBookingResponse { Message = "Schedule is null." };
                    }

                    if (schedule.StartTime < startTime)
                    {
                        startTime = schedule.StartTime;
                    }

                    if (schedule.EndTime > endTime)
                    {
                        endTime = schedule.EndTime;
                    }

                    var pod = await _podRepo.FindByIdAsync(slot.PodId.Value);

                    if (pod == null)
                    {
                        return new GetBookingResponse { Message = "Pod is null." };
                    }

                    podId = pod.Id;
                    podName = pod.Name;
                    podTypeId = pod.PodTypeId;
                    podType = await _podTypeRepo.FindByIdAsync(podTypeId);

                    if (podType == null)
                    {
                        return new GetBookingResponse { Message = "PodType is null." };
                    }
                }

                var bookingOverview = new BookingOverviewDto
                {
                    BookingId = booking.Id,
                    ArrivalDate = arrivalDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    StatusId = booking.BookingStatusId,
                    PodId = podId,
                    PodName = podName,
                    PodTypeId = podTypeId,
                    PodTypeName = podType.Name,
                };

                bookingOverviews.Add(bookingOverview);
            }

            return new GetBookingResponse { BookingOverview = bookingOverviews };
        }

        public async Task<GetBookingResponse> GetBookingById(int id)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return new GetBookingResponse { Success = false, Message = "Unable to find booking with id " + id };
            }

            var user = await _userRepo.FindByIdAsync(booking.UserId);
            booking.User = user;

            var bookingDetails = await _bookingDetailRepo.GetAllAsync();
            var userDetails = bookingDetails.Where(d => d.BookingId == booking.Id).ToList();
            booking.BookingDetails = userDetails;

            foreach (var detail in userDetails)
            {
                var slot = await _slotRepo.FindByIdAsync(detail.SlotId);

                detail.Slot = slot;
            }

            return new GetBookingResponse { Success = true, Booking = booking };
        }

        public async Task<GetBookingResponse> GetUserBookingById(int id, int userId)
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

            var user = await _userRepo.FindByIdAsync(booking.UserId);
            booking.User = user;

            var bookingDetails = await _bookingDetailRepo.GetAllAsync();
            var userDetails = bookingDetails.Where(d => d.BookingId == booking.Id).ToList();
            booking.BookingDetails = userDetails;

            foreach (var detail in userDetails)
            {
                var slot = await _slotRepo.FindByIdAsync(detail.SlotId);

                detail.Slot = slot;
            }

            return new GetBookingResponse { Success = true, Booking = booking };
        }

        public async Task<CancelBookingResponse> CancelBooking(int id, int userId)
        {
            var booking = await _bookingRepo.FindByIdAsync(id);
            var statusId = booking.BookingStatusId;

            if (booking == null)
            {
                return new CancelBookingResponse { Success = false, Message = "The BookingId does not exist." };
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

            var details = await _bookingDetailRepo.GetAllAsync();

            var bookingDetails = details.Where(bd => bd.BookingId == booking.Id);

            foreach (var detail in bookingDetails)
            {
                var slot = await _slotRepo.FindByIdAsync(detail.SlotId);

                slot.Status = 0;

                try
                {
                    await _slotRepo.UpdateAsync(slot);
                }
                catch (Exception ex)
                {
                    return new CancelBookingResponse { Success = false, Message = "Unable to update the slot status." };
                }
            }

            var selectedProducts = await _selectedProductRepo.GetAllAsync();
            var bookingSelectedProducts = selectedProducts.Where(sp => sp.BookingId == booking.Id);

            foreach (var selectedProduct in bookingSelectedProducts)
            {
                var product = await _productRepo.FindByIdAsync(selectedProduct.ProductId);

                if (product != null)
                {
                    product.Quantity += selectedProduct.Quantity;

                    try
                    {
                        await _productRepo.UpdateAsync(product);
                    }
                    catch (Exception ex)
                    {
                        return new CancelBookingResponse { Success = false, Message = "Unable to restore product quantity." };
                    }
                }
            }

            if (statusId == 3)
            {
                var user = await _userRepo.FindByIdAsync(booking.UserId);
                var pointReduced = (int)Math.Floor(booking.ActualPrice / 1000.0);
                user.LoyaltyPoints -= pointReduced;

                while (true)
                {
                    var membershipProgress = await _memberService.GetMembershipProgress(user.Id);
                    var previousMembership = membershipProgress.PreviousMembership;

                    if (previousMembership != null &&
                        previousMembership.PointsRequirement >= 0 &&
                        user.LoyaltyPoints < previousMembership.PointsRequirement)
                    {
                        user.MembershipId = previousMembership.Id;
                    }
                    else
                    {
                        break;
                    }
                }

                try
                {
                    await _userRepo.UpdateAsync(user);
                }
                catch (Exception ex)
                {
                    return new CancelBookingResponse { Success = false, Message = "Unable to update user loyalty point." };
                }
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

            var user = await _userRepo.FindByIdAsync(userId);

            var bookings = await _bookingRepo.GetAllAsync();
            var ongoingBooking = bookings.FirstOrDefault(b => b.UserId == userId && b.BookingStatusId != 1 && b.BookingStatusId != 5);

            if (ongoingBooking != null)
            {
                return new CreateBookingResponse { Success = false, Message = "There can only be one booking at a time." };
            }

            var membership = await _membershipRepo.FindByIdAsync(user.MembershipId.Value);
            var discount = membership.Discount;

            int dayDiff = ((request.ArrivalDate).ToDateTime(TimeOnly.MinValue) - DateTime.UtcNow).Days;

            if (user.MembershipId == 2 && dayDiff < 7)
            {
                return new CreateBookingResponse { Success = false, Message = "The arrival date must be at least 7 days from now." };
            }

            if (user.MembershipId > 2 && dayDiff < 3)
            {
                return new CreateBookingResponse { Success = false, Message = "The arrival date must be at least 3 days from now." };
            }

            if (dayDiff > 30)
            {
                return new CreateBookingResponse { Success = false, Message = "The arrival date must be within a month from now." };
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
                    var bookedSlot = slots.FirstOrDefault(s => s.ArrivalDate == request.ArrivalDate &&
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

            var bookingPrice = (_podTypeRepo.FindByIdAsync(pod.PodTypeId).Result.Price) * request.ScheduleIds.Count;
            var actualPrice = bookingPrice * (1 - (discount / 100.00));

            var booking = new Booking
            {
                BookingPrice = bookingPrice,
                CreatedTime = DateTime.UtcNow,
                BookingStatusId = 2,
                UserId = userId,
                MembershipId = membership.Id,
                Discount = discount,
                ActualPrice = (int)actualPrice
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
                    ArrivalDate = request.ArrivalDate,
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

            booking.BookingStatusId = 3;

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

            var user = await _userRepo.FindByIdAsync(userId);

            int dayDiff = ((request.NewArrivalDate).ToDateTime(TimeOnly.MinValue) - DateTime.UtcNow).Days;

            if (user.MembershipId == 2 && dayDiff < 7)
            {
                return new UpdateBookingResponse { Success = false, Message = "The arrival date must be at least 7 days from now." };
            }

            if (user.MembershipId == 3 && dayDiff < 3)
            {
                return new UpdateBookingResponse { Success = false, Message = "The arrival date must be at least 3 days from now." };
            }

            if (dayDiff > 30)
            {
                return new UpdateBookingResponse { Success = false, Message = "The arrival date must be within a month from now." };
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
                    var newBookedSlot = slots.FirstOrDefault(s => s.ArrivalDate == request.NewArrivalDate &&
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
                    ArrivalDate = request.NewArrivalDate,
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

        public async Task<CheckinBookingResponse> CheckinBooking(int id)
        {
            if (id < 0)
            {
                return new CheckinBookingResponse { Success = false, Message = "BookingId must be given." };
            }

            var booking = await _bookingRepo.FindByIdAsync(id);

            if (booking == null)
            {
                return new CheckinBookingResponse { Success = false, Message = "Unable to find booking with id " + id };
            }

            booking.BookingStatusId = 4;

            try
            {
                await _bookingRepo.UpdateAsync(booking);
            }
            catch (Exception ex)
            {
                return new CheckinBookingResponse { Success = false, Message = "Unable to update status for booking." };
            }

            var user = await _userRepo.FindByIdAsync(booking.UserId);
            var pointGained = (int)Math.Floor(booking.ActualPrice / 1000.0);
            user.LoyaltyPoints += pointGained;

            while (true)
            {
                var membershipProgress = await _memberService.GetMembershipProgress(booking.UserId);
                var nextMembership = membershipProgress.NextMembership;

                if (nextMembership != null && user.LoyaltyPoints >= nextMembership.PointsRequirement)
                {
                    user.MembershipId = nextMembership.Id;
                }
                else
                {
                    break;
                }
            }

            try
            {
                await _userRepo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new CheckinBookingResponse { Success = false, Message = "Unable to update user loyalty point." };
            }

            return new CheckinBookingResponse { Success = true, Booking = booking };
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

            var selectedProducts = await _selectedProductRepo.GetAllAsync();
            var bookingProducts = selectedProducts.Where(sp => sp.BookingId == booking.Id);

            if (bookingProducts.Any())
            {
                var productPrice = 0;

                foreach (var bProduct in bookingProducts)
                {
                    productPrice += (bProduct.ProductPrice * bProduct.Quantity);
                }

                booking.ActualPrice += productPrice;
            }

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
        public async Task<List<BookingOverviewDto>> GetBookingsByAreaIdAsync(int areaId)
        {
            var area = await _areaRepo.FindByIdAsync(areaId);
            if (area == null)
            {
                throw new Exception($"Area with ID {areaId} was not found.");
            }

            var podsInArea = await _podRepo.GetAllAsync();
            var filteredPods = podsInArea.Where(p => p.AreaId == areaId).ToList();
            if (!filteredPods.Any())
            {
                throw new Exception($"No Pods found for Area ID {areaId}.");
            }

            var podIds = filteredPods.Select(p => p.Id).ToList();
            var slotsInPods = await _slotRepo.GetAllAsync();
            var filteredSlots = slotsInPods.Where(s => s.PodId.HasValue && podIds.Contains(s.PodId.Value)).ToList();
            if (!filteredSlots.Any())
            {
                throw new KeyNotFoundException($"No Slots found for Pods in Area ID {areaId}.");
            }

            var slotIds = filteredSlots.Select(s => s.Id).ToList();
            var bookingDetails = await _bookingDetailRepo.GetAllAsync();
            var filteredBookingDetails = bookingDetails.Where(bd => slotIds.Contains(bd.SlotId)).ToList();
            if (!filteredBookingDetails.Any())
            {
                throw new KeyNotFoundException($"No BookingDetails found for Slots in Area ID {areaId}.");
            }

            var bookingIds = filteredBookingDetails.Select(bd => bd.BookingId).Distinct().ToList();
            var allBookings = await _bookingRepo.GetAllAsync();
            var bookings = allBookings.Where(b => bookingIds.Contains(b.Id)).ToList();
            if (!bookings.Any())
            {
                throw new KeyNotFoundException($"No Bookings found for Area ID {areaId}.");
            }

            var bookingOverviews = new List<BookingOverviewDto>();
            foreach (var booking in bookings)
            {
                var arrivalDate = DateOnly.MinValue;
                var startTime = TimeOnly.MaxValue;
                var endTime = TimeOnly.MinValue;
                int podTypeId = 0;
                string podTypeName = string.Empty;

                var userDetails = filteredBookingDetails.Where(d => d.BookingId == booking.Id).ToList();

                foreach (var detail in userDetails)
                {
                    var slot = await _slotRepo.FindByIdAsync(detail.SlotId);
                    if (slot != null)
                    {
                        arrivalDate = slot.ArrivalDate;

                        var schedule = await _scheduleRepo.FindByIdAsync(slot.ScheduleId.Value);
                        if (schedule != null)
                        {
                            if (schedule.StartTime < startTime)
                            {
                                startTime = schedule.StartTime;
                            }

                            if (schedule.EndTime > endTime)
                            {
                                endTime = schedule.EndTime;
                            }

                            var pod = await _podRepo.FindByIdAsync(slot.PodId.Value);
                            if (pod != null)
                            {
                                podTypeId = pod.PodTypeId;
                                var podType = await _podTypeRepo.FindByIdAsync(podTypeId);
                                if (podType != null)
                                {
                                    podTypeName = podType.Name;
                                }
                            }
                        }
                    }
                }

                var bookingOverview = new BookingOverviewDto
                {
                    BookingId = booking.Id,
                    ArrivalDate = arrivalDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    StatusId = booking.BookingStatusId,
                    PodTypeId = podTypeId,
                    PodTypeName = podTypeName,
                };

                bookingOverviews.Add(bookingOverview);
            }
            return bookingOverviews;
        }
    }
}
