using AutoMapper;

namespace OrderSchemaAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //CreateMap<Models.Order, Common.Order>();
            // CreateMap<Common.Order, Models.Order>();
           // CreateMap<IEnumerable<Models.Order>, IEnumerable<Common.Order>>();
            CreateMap<IEnumerable<Common.OrderResponse>, List<Models.Order>>();
                /*.ForMember(
                    dest => dest.RequestedPickupTime,
                    opt => opt.MapFrom(src => $"{src.RequestedPickupTime}")
                )
                .ForMember(
                    dest => dest.PickupAddress,
                    opt => opt.MapFrom(src => $"{src.PickupAddress}")
                )
                .ForMember(
                    dest => dest.DeliveryAddress,
                    opt => opt.MapFrom(src => $"{src.DeliveryAddress}")
                )
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src => $"{src.Items}")
                )
                .ForMember(
                    dest => dest.PickupInstructions,
                    opt => opt.MapFrom(src => $"{src.PickupInstructions}")
                )
                .ForMember(
                    dest => dest.DeliveryInstructions,
                    opt => opt.MapFrom(src => $"{src.DeliveryInstructions}")
                );*/
        }
    }
}
