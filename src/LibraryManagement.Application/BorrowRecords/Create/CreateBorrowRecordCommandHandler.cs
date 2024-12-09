using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using LibraryManagement.Domain.PatronAggregate;
using LibraryManagement.Domain.PatronTypeAggregate;
using MapsterMapper;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.Create;

public class CreateBorrowRecordCommandHandler: IRequestHandler<CreateBorrowRecordCommand, ErrorOr<CreateBorrowRecordDto>>
{
    private readonly IBaseRepository<BorrowRecord> _borrowRecordRepository;
    private readonly IBaseRepository<Book> _bookRepository;

    private readonly IBaseRepository<Patron> _patronRepository;
    private readonly IBaseRepository<PatronType> _patronTypeRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public CreateBorrowRecordCommandHandler(IBaseRepository<BorrowRecord> borrowRecordRepository, IDateTimeProvider dateTimeProvider, IBaseRepository<Book> bookRepository, IMapper mapper, IBaseRepository<PatronType> patronTypeRepository, IBaseRepository<Patron> patronRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _dateTimeProvider = dateTimeProvider;
        _bookRepository = bookRepository;
        _mapper = mapper;
        _patronTypeRepository = patronTypeRepository;
        _patronRepository = patronRepository;
    }

    public async Task<ErrorOr<CreateBorrowRecordDto>> Handle(CreateBorrowRecordCommand request,
        CancellationToken cancellationToken)
    {
        var patron = await _patronRepository.FindAsync(request.PatronId);

        if(patron == null)
            return Error.NotFound("Patron not found");

        var patronType = await _patronTypeRepository.FindAsync(patron.TypeId.Value);

        if(patronType == null)
            return Error.NotFound("Patron type not found");

        var borrowRecord = BorrowRecord.Create(request.PatronId, _dateTimeProvider.Now
            , request.DueDate,
             request.BorrowRecordBooksInfo.Select(bookInfo
              => BorrowRecordBookId.Create(bookInfo.BookId, bookInfo.BookCopyIds)).ToList(),
              patronType.BookRentalFee);
        
        _borrowRecordRepository.Add(borrowRecord);
        
        await _borrowRecordRepository.SaveChangeAsync();
        
        return _mapper.Map<CreateBorrowRecordDto>(borrowRecord);
    }
}