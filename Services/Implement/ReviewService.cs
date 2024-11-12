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
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryBase<Review> _reviewRepo;
        private readonly IRepositoryBase<Booking> _bookingRepo;
        private readonly IRepositoryBase<User> _userRepo;
        private readonly IRepositoryBase<BookingDetail> _bookingDetailRepo;
        private readonly IRepositoryBase<Slot> _slotRepo;
        private readonly IRepositoryBase<Pod> _podRepo;
        private readonly IRepositoryBase<PodType> _podTypeRepo;

        public ReviewService(IRepositoryBase<Review> reviewRepo, IRepositoryBase<Booking> bookingRepo,
            IRepositoryBase<User> userRepo, IRepositoryBase<BookingDetail> bookingDetailRepo,
            IRepositoryBase<Slot> slotRepo, IRepositoryBase<Pod> podRepo, IRepositoryBase<PodType> podTypeRepo)
        {
            _reviewRepo = reviewRepo;
            _bookingRepo = bookingRepo;
            _userRepo = userRepo;
            _bookingDetailRepo = bookingDetailRepo;
            _slotRepo = slotRepo;
            _podRepo = podRepo;
            _podTypeRepo = podTypeRepo;
        }

        public async Task<Review> CreateReviewAsync(CreateReviewRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Review cannot be null.");
            }

            if (request.Rating < 1 || request.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            if (string.IsNullOrWhiteSpace(request.Text))
            {
                throw new ArgumentException("Review text cannot be empty.");
            }

            var review = new Review
            {
                Rating = request.Rating,
                Text = request.Text,
                BookingId = request.BookingId,
                Status = 1,
            };

            await _reviewRepo.AddAsync(review);
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid review ID.");
            }

            var review = await _reviewRepo.FindByIdAsync(id);
            if (review == null)
            {
                throw new ArgumentException("Review not found.");
            }

            await _reviewRepo.DeleteAsync(review);
            return true;
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid review ID.");
            }

            var review = await _reviewRepo.FindByIdAsync(id);
            if (review == null)
            {
                throw new ArgumentException("Review not found.");
            }

            return review;
        }

        public async Task<List<GetReviewResponse>> GetReviewsAsync()
        {
            var getReviewResponses = new List<GetReviewResponse>();

            var reviews = await _reviewRepo.GetAllAsync();
            if (reviews != null)
            {
                foreach (var review in reviews)
                {
                    var booking = await _bookingRepo.FindByIdAsync(review.BookingId);
                    if (booking != null)
                    {
                        var customer = await _userRepo.FindByIdAsync(booking.UserId);
                        var customerName = customer.Name;
                        var bookingDetail = _bookingDetailRepo.GetAllAsync().Result.FirstOrDefault(bd => bd.BookingId == booking.Id);
                        if (bookingDetail != null)
                        {
                            var slot = _slotRepo.GetAllAsync().Result.FirstOrDefault(s => s.BookingDetailId == bookingDetail.BookingId);
                            if (slot != null)
                            {
                                var arrivalDate = slot.ArrivalDate;
                                var pod = await _podRepo.FindByIdAsync(slot.PodId.Value);
                                if (pod != null)
                                {
                                    var podType = await _podTypeRepo.FindByIdAsync(pod.PodTypeId);
                                    if (podType != null)
                                    {
                                        var podTypeId = podType.Id;
                                        var podTypeName = podType.Name;
                                        var reviewResponse = new GetReviewResponse
                                        {
                                            ReviewId = review.Id,
                                            Rating = review.Rating,
                                            Text = review.Text,
                                            CustomerName = customerName,
                                            ArrivalDate = arrivalDate,
                                            PodTypeId = podTypeId,
                                            PodTypeName = podTypeName,
                                        };
                                        getReviewResponses.Add(reviewResponse);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return getReviewResponses;
        }

        public async Task<Review> UpdateReviewAsync(int id, CreateReviewRequest request)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid review ID.");
            }

            var existingReview = await _reviewRepo.FindByIdAsync(id);
            if (existingReview == null)
            {
                throw new ArgumentException("Review not found.");
            }

            if (request.Rating < 1 || request.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            if (string.IsNullOrWhiteSpace(request.Text))
            {
                throw new ArgumentException("Review text cannot be empty.");
            }

            existingReview.Rating = request.Rating;
            existingReview.Text = request.Text;
            //existingReview.BookingId = request.BookingId;

            await _reviewRepo.UpdateAsync(existingReview);
            return existingReview;
        }

        public async Task<Review> GetReviewByBookingIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid review ID.");
            }

            var booking = await _bookingRepo.FindByIdAsync(id);
            if (booking == null)
            {
                throw new ArgumentException("Invalid booking ID.");
            }

            var review = _reviewRepo.GetAllAsync().Result.FirstOrDefault(r => r.BookingId == booking.Id);
            if (review == null)
            {
                throw new ArgumentException("Review not found.");
            }

            return review;
        }
    }
}
