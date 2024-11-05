using FluentValidation;

namespace LibraryManagement.Application.BorrowRecords.Create;

public class CreateBorrowRecordValidator : AbstractValidator<CreateBorrowRecordCommand>
{
    public CreateBorrowRecordValidator()
    {
        
    }


    private bool CheckIbsnValid(string ibsn)
    {
        return (ibsn.Length == 13 && ibsn.All(char.IsDigit)) || (ibsn.Length == 10 && ibsn.All(c => char.IsLetterOrDigit(c)));
    }
}