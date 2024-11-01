using AutoMapper;
using PRM392_BookSoccerYard.API.DTO.Customer;
using PRM392_BookSoccerYard.API.DTO.Order;
using PRM392_BookSoccerYard.API.DTO.Service;
using PRM392_BookSoccerYard.API.DTO.Slot;
using PRM392_BookSoccerYard.API.DTO.Yard;
using PRM392_BookSoccerYard.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PRM392_BookSoccerYard.API
{
    public class Mappers : Profile
    {
        public Mappers() {

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, SignUp>().ReverseMap();

            CreateMap<Yard, YardDTO>().ReverseMap();
            CreateMap<Yard, CreatedYard>().ReverseMap();

            CreateMap<Slot, SlotDTO>().ReverseMap();
            CreateMap<Slot,CreatedSlot>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<CreatedOrder, Order>().ReverseMap()
                .ForMember(x => x.orderDetails, otp => otp.MapFrom(x => x.OrderDetails));
            CreateMap<OrderDetail,CreatedOrderDetail>().ReverseMap();
            CreateMap<Payment,CreatedPayment>().ReverseMap();

            CreateMap<Service,ServiceDTO>().ReverseMap();
            CreateMap<Service,CreatedService>().ReverseMap();

        }
    }
}
