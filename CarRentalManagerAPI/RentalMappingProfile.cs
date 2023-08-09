using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Car;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.Rental;
using CarRentalManagerAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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

            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.DrivingLicenseCategories, opt => opt.MapFrom(src => StringToDrivingLicenseCategoriesEnumList(src.DrivingLicenseCategories)));

            CreateMap<CreateClientDto, Client>()
                .ForMember(dest => dest.DrivingLicenseCategories, opt => opt.MapFrom(src => DrivingLicenseCategoriesEnumArrToString(src.DrivingLicenseCategories)))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => FirstLetterToUpper(src.Surname)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => FirstLetterToUpper(src.Name)))
                .ForMember(dest => dest.PESELOrPassportNumber, opt => opt.MapFrom(src => src.PESELOrPassportNumber.ToUpper()));

            CreateMap<UpdateClientDto, Client>()
                .ForMember(dest => dest.DrivingLicenseCategories, opt => opt.MapFrom(src => DrivingLicenseCategoriesEnumArrToString(src.DrivingLicenseCategories)))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => FirstLetterToUpper(src.Surname)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => FirstLetterToUpper(src.Name)))
                .ForMember(dest => dest.PESELOrPassportNumber, opt => opt.MapFrom(src => src.PESELOrPassportNumber.ToUpper()));

            CreateMap<User, UserDto>();

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => FirstLetterToUpper(src.Name)))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => FirstLetterToUpper(src.Surname)));

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => FirstLetterToUpper(src.Name)))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => FirstLetterToUpper(src.Surname)));

            CreateMap<Rental, RentalDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => src.RentalDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")))
                .ForMember(dest => dest.DateOfReturn, opt => opt.MapFrom(src => src.DateOfReturn.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")))
                .ForMember(dest => dest.ExpectedDateOfReturn, opt => opt.MapFrom(src => src.ExpectedDateOfReturn.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")));

            CreateMap<CreateRentalDto, Rental>()
                .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.RentalDate, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None)))
                .ForMember(dest => dest.ExpectedDateOfReturn, opt => opt.MapFrom(src => DateTime.ParseExact(src.ExpectedDateOfReturn, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None)));
        }

        private List<string> StringToDrivingLicenseCategoriesEnumList(string str)
        {
            var result = str.Split(';').ToList();
            return result;
        }

        private string DrivingLicenseCategoriesEnumArrToString(string[] arr)
        {
            var enums = arr.Select(c => Enum.Parse<DrivingLicenseCategoryEnum>(c, true).ToString());
            return string.Join(";", enums);
        }

        private string FirstLetterToUpper(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(char.ToUpper(str[0]));
            sb.Append(str.Substring(1));
            
            return sb.ToString();
        }
    }
}
