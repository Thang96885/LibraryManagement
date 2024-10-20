using LibraryManagement.Application.Patrons.Get;
using LibraryManagement.Application.Patrons.List;
using LibraryManagement.Domain.PatronAggregate;
using Mapster;

namespace LibraryManagement.Application.Common.MappingConfiguration
{
	public class PatronMappingConfiguration : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Patron, ListPatronDto>()
				.Map(dest => dest.Id, src => src.Id.ToString())
				.Map(dest => dest.Name, src => src.Name)
				.Map(dest => dest.Email, src => src.Email)
				.Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
				.Map(dest => dest.Street, src => src.Address.Street)
				.Map(dest => dest.City, src => src.Address.City)
				.Map(dest => dest.State, src => src.Address.State)
				.Map(dest => dest.BorrowRecordCount, src => src.BorrowRecordIds.Count.ToString())
				.Map(dest => dest.ReturnRecordCount, src => src.ReturnRecordIds.Count.ToString());

			config.NewConfig<Patron, GetPatronDto>()
				.Map(dest => dest.Id, src => src.Id.ToString())
				.Map(dest => dest.Name, src => src.Name)
				.Map(dest => dest.Email, src => src.Email)
				.Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
				.Map(dest => dest.Street, src => src.Address.Street)
				.Map(dest => dest.City, src => src.Address.City)
				.Map(dest => dest.State, src => src.Address.State)
				.Map(dest => dest.ZipCode, src => src.Address.ZipCode)
				.Map(dest => dest.BorrowRecordCount, src => src.BorrowRecordIds.Count.ToString())
				.Map(dest => dest.ReturnRecordCount, src => src.ReturnRecordIds.Count.ToString())
				.Map(dest => dest.ReservationCount, src => src.ReservationIds.Count.ToString());
		}
	}
}
