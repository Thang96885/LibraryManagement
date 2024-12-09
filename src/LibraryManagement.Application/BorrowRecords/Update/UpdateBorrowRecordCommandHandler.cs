using ErrorOr;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.PatronTypeAggregate;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.Update;

public class UpdateBorrowRecordCommandHandler : IRequestHandler<UpdateBorrowRecordCommand, ErrorOr<bool>>
{
    private readonly IBaseRepository<BorrowRecord> _borrowRecordRepository;
    private readonly IBaseRepository<Patron> _patronRepository;
    private readonly IBaseRepository<PatronType> _patronTypesRepository;
    public UpdateBorrowRecordCommandHandler(IBaseRepository<BorrowRecord> borrowRecordRepository, IBaseRepository<Patron> patronRepository, IBaseRepository<PatronType> patronTypesRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _patronRepository = patronRepository;
        _patronTypesRepository = patronTypesRepository;
    }

    public async Task<ErrorOr<bool>> Handle(UpdateBorrowRecordCommand request, CancellationToken cancellationToken)
    {
        var borrowRecord = await _borrowRecordRepository.FindAsync(request.BorrowRecordId);
        
        if(borrowRecord == null)
            return Error.NotFound("Borrow record could not be found");

        var patron = await _patronRepository.FindAsync(borrowRecord.PatronId.Value);

        if(patron == null)
            return Error.NotFound("Patron could not be found");

        var patronType = await _patronTypesRepository.FindAsync(patron.TypeId.Value);

        if(patronType == null)
            return Error.NotFound("Patron type could not be found");

        borrowRecord.Update(request.DueDate, patronType.BookRentalFee);        
        
        _borrowRecordRepository.Update(borrowRecord);

        await _borrowRecordRepository.SaveChangeAsync();

        return true;
    }
}