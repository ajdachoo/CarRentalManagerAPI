using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Car;

namespace CarRentalManagerAPI
{
    public class RentalMappingProfile : Profile
    {
        public RentalMappingProfile()
        {
            CreateMap<Car, CarDto>();

            CreateMap<CreateCarDto, Car>();
        }
    }
}
