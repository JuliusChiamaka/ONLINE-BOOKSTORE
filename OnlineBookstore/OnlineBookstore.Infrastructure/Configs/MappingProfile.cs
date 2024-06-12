using AutoMapper;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using OnlineBookstore.Domain.Entities;

namespace OnlineBookstore.Infrastructure.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookResponse>().ReverseMap();
            CreateMap<AddBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>();

            CreateMap<AppUser, UserResponse>().ReverseMap();
            CreateMap<AddUserRequest, AppUser>();
            CreateMap<UpdateUserRequest, AppUser>();

            CreateMap<CartItem, CartItemResponse>().ReverseMap();
            CreateMap<AddToCartRequest, CartItem>();

            CreateMap<PurchaseHistory, PurchaseHistoryResponse>().ReverseMap();
            CreateMap<AddPurchaseHistoryRequest, PurchaseHistory>();
            CreateMap<PurchaseHistoryItem, PurchaseHistoryItemResponse>().ReverseMap();
            CreateMap<AddPurchaseHistoryItemRequest, PurchaseHistoryItem>();

            CreateMap<PurchaseHistory, PurchaseHistoryDto>();
            CreateMap<PurchaseHistoryItem, PurchaseHistoryItemDto>();
            CreateMap<Book, BookDto>();

            // DTO to Response
            CreateMap<PurchaseHistoryDto, PurchaseHistoryResponse>();
            CreateMap<PurchaseHistoryItemDto, PurchaseHistoryItemResponse>();
            CreateMap<BookDto, BookResponse>();
        }
    }

}
