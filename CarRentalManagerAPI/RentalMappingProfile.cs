using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Car;
using System;

namespace CarRentalManagerAPI
{
    public class RentalMappingProfile : Profile
    {
        public RentalMappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DrivingLicenseCategory, opt => opt.MapFrom(src => src.DrivingLicenseCategory.ToString()))
                .ForMember(dest => dest.Transmission, opt => opt.MapFrom(src => src.Transmission.ToString()));

            CreateMap<CreateCarDto, Car>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<CarStatusEnum>(src.Status, true)))
                .ForMember(dest => dest.DrivingLicenseCategory, opt => opt.MapFrom(src => Enum.Parse<DrivingLicenseCategoryEnum>(src.DrivingLicenseCategory, true)))
                .ForMember(dest => dest.Transmission, opt => opt.MapFrom(src => Enum.Parse<TransmissionEnum>(src.Transmission, true)))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.VIN.ToUpper()))
                .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber.ToUpper()));

            CreateMap<UpdateCarDto, Car>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<CarStatusEnum>(src.Status, true)))
                .ForMember(dest => dest.DrivingLicenseCategory, opt => opt.MapFrom(src => Enum.Parse<DrivingLicenseCategoryEnum>(src.DrivingLicenseCategory, true)))
                .ForMember(dest => dest.Transmission, opt => opt.MapFrom(src => Enum.Parse<TransmissionEnum>(src.Transmission, true)))
                .ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.VIN.ToUpper()))
                .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber.ToUpper()));
        }
    }
}
