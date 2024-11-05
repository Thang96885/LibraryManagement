using ErrorOr;
using LibraryManagement.Domain.BookAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate;
using LibraryManagement.Domain.BorrowRecordAggregate.ValueObjects;
using LibraryManagement.Domain.Common.Interface;
using MapsterMapper;
using MediatR;

namespace LibraryManagement.Application.BorrowRecords.Create;

public class CreateBorrowRecordCommandHandler: IRequestHandler<CreateBorrowRecordCommand, ErrorOr<CreateBorrowRecordDto>>
{
    private readonly IBaseRepository<BorrowRecord> _borrowRecordRepository;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public CreateBorrowRecordCommandHandler(IBaseRepository<BorrowRecord> borrowRecordRepository, IDateTimeProvider dateTimeProvider, IBaseRepository<Book> bookRepository, IMapper mapper)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _dateTimeProvider = dateTimeProvider;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CreateBorrowRecordDto>> Handle(CreateBorrowRecordCommand request,
        CancellationToken cancellationToken)
    {
        var borrowRecord = BorrowRecord.Create(request.PatronId, _dateTimeProvider.Now
            , request.DueDate, request.BorrowRecordBooksInfo.Select(bookInfo => BorrowRecordBookId.Create(bookInfo.BookId, bookInfo.BookCopyIds)).ToList());
        _borrowRecordRepository.Add(borrowRecord);
        
        await _borrowRecordRepository.SaveChangeAsync();
        
        return _mapper.Map<CreateBorrowRecordDto>(borrowRecord);
    }
}